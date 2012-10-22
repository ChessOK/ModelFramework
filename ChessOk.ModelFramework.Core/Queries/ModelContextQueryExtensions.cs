using System;

using ChessOk.ModelFramework.Queries.Internals;

namespace ChessOk.ModelFramework
{
    public static class ModelContextQueryExtensions
    {
        public static T Query<T>(this ModelContext context)
            where T : Query
        {
            return context.Query<T>(null);
        }

        public static T Query<T>(this ModelContext context, Action<T> initialization)
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
