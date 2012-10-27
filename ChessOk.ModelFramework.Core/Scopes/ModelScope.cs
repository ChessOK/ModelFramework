using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework.Scopes
{
    public class ModelScope : IModelScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        public ModelScope(IModelScope parentScope, object tag, Action<ContainerBuilder> configuration)
            : this(parentScope.LifetimeScope, tag, configuration)
        {
        }

        internal ModelScope(ILifetimeScope parentLifetimeScope, object tag, Action<ContainerBuilder> configuration)
        {
            if (parentLifetimeScope == null)
            {
                throw new ArgumentNullException("parentLifetimeScope");
            }

            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            var scope = parentLifetimeScope.BeginLifetimeScope(
                tag, builder =>
                    {
                        builder.RegisterInstance(this).As<IModelScope>().AsSelf();
                        builder.Register(x => new ModelScopeCache()).SingleInstance();

                        if (configuration != null) { configuration(builder); }
                    });

            _lifetimeScope = scope;
        }

        public ILifetimeScope LifetimeScope
        {
            get { return _lifetimeScope; }
        }

        public T Get<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }

        public object Get(Type serviceType)
        {
            return _lifetimeScope.Resolve(serviceType);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _lifetimeScope.Resolve<IEnumerable<T>>();
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return (IEnumerable<object>)_lifetimeScope.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }

        public virtual void Dispose()
        {
            _lifetimeScope.Dispose();
        }
    }
}
