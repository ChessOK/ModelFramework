using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework
{
    public interface IModelContext : IDisposable
    {
        T Get<T>();
        object Get(Type serviceType);

        IEnumerable<T> GetAll<T>();
        IEnumerable<object> GetAll(Type serviceType);

        IModelContext CreateChildContext(object tag, Action<ContainerBuilder> registrations);
    }
}