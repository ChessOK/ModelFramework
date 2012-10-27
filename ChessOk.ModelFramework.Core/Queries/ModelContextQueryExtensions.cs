using System;

using ChessOk.ModelFramework.Queries.Internals;
using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework
{
    public static class ModelScopeQueryExtensions
    {
        public static T Query<T>(this IModelScope model)
            where T : Query
        {
            return model.Query<T>(null);
        }

        public static T Query<T>(this IModelScope model, Action<T> initialization)
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
