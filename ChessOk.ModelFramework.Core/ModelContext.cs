using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Представляет собой <see cref="IModelContext"/> самого верхнего
    /// уровня. В пределах <see cref="ModelContext"/> регистрируются
    /// все классы, имеющие отношение к модели приложения.
    /// <seealso cref="RegistrationExtensions"/>
    /// </summary>
    public class ModelContext : IModelContext
    {
        private readonly ILifetimeScope _lifetimeScope;

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
            _lifetimeScope = parentScope.BeginLifetimeScope(tag, configurationAction);
        }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        /// <summary>
        /// Получить экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип получаемого сервиса.</typeparam>
        /// <returns>Экземпляр сервиса.</returns>
        public T Get<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }

        /// <summary>
        /// Получить экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип получаемого сервиса.</param>
        /// <returns>Экземпляр сервиса.</returns>
        public object Get(Type serviceType)
        {
            return _lifetimeScope.Resolve(serviceType);
        }

        /// <summary>
        /// Получить коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип сервисов.</typeparam>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        public IEnumerable<T> GetAll<T>()
        {
            return _lifetimeScope.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Получить коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервисов.</param>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        public IEnumerable<object> GetAll(Type serviceType)
        {
            return (IEnumerable<object>)_lifetimeScope.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }

        public IModelContext CreateChildContext(object tag, Action<ContainerBuilder> configurationAction)
        {
            return new ModelContext(_lifetimeScope, tag, configurationAction);
        }
    }
}
