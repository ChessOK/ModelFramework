using System;

using ChessOk.ModelFramework.Queries.Internals;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет расширения к интерфейсу <see cref="IModelContext"/>
    /// для осуществления запросов <see cref="Queries.Query{TResult}"/>.
    /// </summary>
    public static class ModelContextQueryExtensions
    {
        /// <summary>
        /// Получает зарегистрированный в контейнере экземпляр запроса <typeparamref name="T"/>
        /// и выполняет его.
        /// </summary>
        /// <typeparam name="T">Тип запроса.</typeparam>
        /// <param name="context"></param>
        /// <returns>Выполненный запрос.</returns>
        public static T Query<T>(this IModelContext context)
            where T : Query
        {
            return context.Query<T>(null);
        }

        /// <summary>
        /// Получает зарегистрированный в контейнере экземпляр запроса <typeparamref name="T"/>,
        /// инициализирует его с помощью <paramref name="initialization"/> и выполняет его.
        /// </summary>
        /// <typeparam name="T">Тип запроса.</typeparam>
        /// <param name="context"></param>
        /// <param name="initialization">Действия по инициализации запроса.</param>
        /// <returns>Выполненный запрос.</returns>
        public static T Query<T>(this IModelContext context, Action<T> initialization)
            where T : Query
        {
            var query = context.Get<T>();

            if (initialization != null)
            {
                initialization(query);
            }

            query.Bind(context);
            query.Invoke();
            return query;
        }
    }
}
