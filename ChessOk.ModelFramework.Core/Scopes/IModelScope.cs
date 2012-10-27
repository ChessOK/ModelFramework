using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework.Scopes
{
    public interface IModelScope : IDisposable
    {
        T Get<T>();
        object Get(Type serviceType);

        ILifetimeScope LifetimeScope { get; }

        IEnumerable<T> GetAll<T>();
        IEnumerable<object> GetAll(Type serviceType);
    }
}