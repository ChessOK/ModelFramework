using System;
using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Представляет собой <see cref="IModelScope"/> самого верхнего
    /// уровня. В пределах <see cref="ModelContext"/> регистрируются
    /// все классы, имеющие отношение к модели приложения.
    /// <seealso cref="RegistrationExtensions"/>
    /// </summary>
    public class ModelContext : IModelScope
    {
        private readonly IModelScope _context;

        /// <summary>
        /// Инициализирует экземпляр класса <seealso cref="ModelContext"/>,
        /// используя указанный <see cref="ILifetimeScope"/>.
        /// </summary>
        /// <param name="parentScope"></param>
        public ModelContext(ILifetimeScope parentScope)
        {
            _context = new ModelScope(parentScope, ScopeHierarchy.ModelContext, x => x.RegisterInstance(this).AsSelf());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Получить экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип получаемого сервиса.</typeparam>
        /// <returns>Экземпляр сервиса.</returns>
        public T Get<T>()
        {
            return _context.Get<T>();
        }

        /// <summary>
        /// Получить экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип получаемого сервиса.</param>
        /// <returns>Экземпляр сервиса.</returns>
        public object Get(Type serviceType)
        {
            return _context.Get(serviceType);
        }

        /// <summary>
        /// Получить экземпляр <see cref="ILifetimeScope"/>,
        /// ассоциированный с данным экземпляром класса <see cref="ModelContext"/>.
        /// </summary>
        public ILifetimeScope LifetimeScope
        {
            get
            {
                return _context.LifetimeScope;
            }
        }

        /// <summary>
        /// Получить коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип сервисов.</typeparam>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        public IEnumerable<T> GetAll<T>()
        {
            return _context.GetAll<T>();
        }

        /// <summary>
        /// Получить коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервисов.</param>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _context.GetAll(serviceType);
        }
    }
}
