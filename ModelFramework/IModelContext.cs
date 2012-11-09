using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Служит оболочкой для <see cref="ILifetimeScope"/>,
    /// предоставляя упрощенную регистрацию для классов модели
    /// приложения. См. <see cref="ModelContext"/>.
    /// </summary>
    public interface IModelContext : IDisposable
    {
        /// <summary>
        /// Возвращает экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип получаемого сервиса.</typeparam>
        /// <returns>Экземпляр сервиса.</returns>
        T Get<T>();

        /// <summary>
        /// Возвращает экземпляр сервиса, зарегистрированного
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип получаемого сервиса.</param>
        /// <returns>Экземпляр сервиса.</returns>
        object Get(Type serviceType);

        /// <summary>
        /// Возвращает коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип сервисов.</typeparam>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Возвращает коллекцию экземпляров сервисов, зарегистрированных
        /// в контейнере под типом <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервисов.</param>
        /// <returns>Коллекция экземпляров сервисов.</returns>
        IEnumerable<object> GetAll(Type serviceType);

        /// <summary>
        /// Создает дочерний экземпляр <see cref="IModelContext"/>, в котором будут доступны
        /// все зарегистрированные в текущем экземпляре классы модели,
        /// предоставляя возможность для регистрации новых.
        /// <para>В дочернем экземпляре будет создан дочерний <see cref="ILifetimeScope"/>
        /// с тегом <paramref name="tag"/> и <paramref name="registrations"/>.</para>
        /// </summary>
        /// <param name="tag">Тэг экземпляра <see cref="ILifetimeScope"/>.</param>
        /// <param name="registrations">Действия по регистрации дополнительных классов.</param>
        /// <returns></returns>
        IModelContext CreateChildContext(object tag, Action<ContainerBuilder> registrations);

        /// <summary>
        /// Получает <see cref="LifetimeScope"/>, ассоциированный с данным экземпляром <see cref="IModelContext"/>.
        /// </summary>
        ILifetimeScope LifetimeScope { get; }
    }
}