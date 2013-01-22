using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет возможности по получению экземпляров компонентов модели,
    /// зарегистрированных в контейнере.
    /// </summary>
    /// 
    /// <remarks>
    /// Представляет собой <see cref="IModelContext"/> самого верхнего
    /// уровня. В пределах <see cref="ModelContext"/> регистрируются
    /// все классы, имеющие отношение к модели приложения.
    /// <seealso cref="RegistrationExtensions"/>
    /// </remarks>
    public class ModelContext : IModelContext
    {
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ModelContext"/>,
        /// используя <paramref name="parentScope"/>.
        /// </summary>
        /// <param name="parentScope">Autofac lifetime scope.</param>
        public ModelContext(ILifetimeScope parentScope)
        {
            _lifetimeScope = parentScope.BeginLifetimeScope(
                ContextHierarchy.ModelContext, 
                builder =>
                    {
                        builder.RegisterInstance(this).As<IModelContext>().AsSelf();
                        builder.Register(x => new ModelContextCache()).SingleInstance();
                    });
        }

        internal ModelContext(ILifetimeScope parentScope, object tag, Action<ContainerBuilder> configurationAction)
        {
            _lifetimeScope = parentScope.BeginLifetimeScope(tag, builder =>
                {
                    builder.RegisterInstance(this).As<IModelContext>().AsSelf();
                    configurationAction(builder);
                });
        }

        public ILifetimeScope LifetimeScope { get { return _lifetimeScope; } }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        public T Get<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Get(Type serviceType)
        {
            return Resolve(serviceType);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return (IEnumerable<T>)Resolve(typeof(IEnumerable<T>));
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return (IEnumerable<object>)Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }

        public IModelContext CreateChildContext(object tag, Action<ContainerBuilder> configurationAction)
        {
            return new ModelContext(_lifetimeScope, tag, configurationAction);
        }

        private object Resolve(Type type)
        {
            var obj = _lifetimeScope.Resolve(type);

            var hasModelContext = obj as IHaveModelContext;
            if (hasModelContext != null)
            {
                hasModelContext.Context = this;
            }

            return obj;
        }
    }
}
