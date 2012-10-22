using System;
using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.Contexts;

namespace ChessOk.ModelFramework
{
    public class ModelContext : IContext
    {
        private readonly IContext _context;

        public ModelContext(ILifetimeScope parentScope)
        {
            _context = new Context(parentScope, ContextHierarchy.ModelContext, x => x.RegisterInstance(this).AsSelf());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public T Get<T>()
        {
            return _context.Get<T>();
        }

        public object Get(Type service)
        {
            return _context.Get(service);
        }

        public ILifetimeScope Scope
        {
            get
            {
                return _context.Scope;
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
