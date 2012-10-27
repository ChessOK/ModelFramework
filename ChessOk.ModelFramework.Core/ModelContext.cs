using System;
using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework
{
    public class ModelContext : IModelScope
    {
        private readonly IModelScope _context;

        public ModelContext(ILifetimeScope parentScope)
        {
            _context = new ModelScope(parentScope, ScopeHierarchy.ModelContext, x => x.RegisterInstance(this).AsSelf());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public T Get<T>()
        {
            return _context.Get<T>();
        }

        public object Get(Type serviceType)
        {
            return _context.Get(serviceType);
        }

        public ILifetimeScope LifetimeScope
        {
            get
            {
                return _context.LifetimeScope;
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _context.GetAll<T>();
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _context.GetAll(serviceType);
        }
    }
}
