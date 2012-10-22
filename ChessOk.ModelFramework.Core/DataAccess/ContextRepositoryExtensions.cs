using ChessOk.ModelFramework.Contexts;

namespace ChessOk.ModelFramework
{
    public static class ContextRepositoryExtensions
    {
        public static IRepository<TEntity> GetRepository<TEntity>(this IContext context)
            where TEntity : Entity<int>
        {
            return context.Get<IRepository<TEntity>>();
        }

        public static IMultiRepository<TEntity> GetMultiRepository<TEntity>(this IContext context)
            where TEntity : Entity<int>
        {
            return context.Get<IMultiRepository<TEntity>>();
        }
    }
}
