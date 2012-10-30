using System;

using ChessOk.ModelFramework.Queries.Internals;

namespace ChessOk.ModelFramework
{
    public static class ModelContextQueryExtensions
    {
        public static T Query<T>(this IModelContext model)
            where T : Query
        {
            return model.Query<T>(null);
        }

        public static T Query<T>(this IModelContext model, Action<T> initialization)
            where T : Query
        {
            var query = model.Get<T>();

            if (initialization != null)
            {
                initialization(query);
            }

            query.Bind(model);
            query.Invoke();
            return query;
        }
    }
}
