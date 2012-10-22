using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework.Contexts
{
    public interface IContext : IDisposable
    {
        T Get<T>();
        object Get(Type service);

        ILifetimeScope Scope { get; }

        IEnumerable<T> GetAll<T>();
        IEnumerable<object> GetAll(Type serviceType);
    }
}