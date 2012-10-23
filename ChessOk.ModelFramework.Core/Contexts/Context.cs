using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework.Contexts
{
    public class Context : IContext
    {
        private readonly ILifetimeScope _lifetimeScope;

        public Context(IContext parentContext, object tag, Action<ContainerBuilder> configuration)
            : this(parentContext.Scope, tag, configuration)
        {
        }

        public Context(ILifetimeScope parentScope, object tag, Action<ContainerBuilder> configuration)
        {
            if (parentScope == null)
            {
                throw new ArgumentNullException("parentScope");
            }

            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            var contextScope = parentScope.BeginLifetimeScope(
                tag, builder =>
                    {
                        builder.RegisterInstance(this).As<IContext>().AsSelf();
                        builder.Register(x => new ContextCache()).SingleInstance();

                        if (configuration != null) { configuration(builder); }
                    });

            _lifetimeScope = contextScope;
        }

        public ILifetimeScope Scope
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
