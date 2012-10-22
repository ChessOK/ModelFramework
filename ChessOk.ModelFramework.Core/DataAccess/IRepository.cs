using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessOk.ModelFramework
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> ItemsQuery { get; }

        TEntity GetById(int id);
        ReadOnlyCollection<TEntity> GetByIdList(IEnumerable<int> ids);
        ReadOnlyCollection<TEntity> GetAll();

        void Delete(TEntity entity);
        void Save(TEntity entity);
    }

    public interface IMultiRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TDerivedEntity> GetDerivedItemsQuery<TDerivedEntity>() where TDerivedEntity : TEntity;

        TDerivedEntity GetById<TDerivedEntity>(int id) where TDerivedEntity : TEntity;

        ReadOnlyCollection<TDerivedEntity> GetByIdList<TDerivedEntity>(IEnumerable<int> idList)
            where TDerivedEntity : TEntity;

        IQueryable<TEntity> ApplyFilter<TDerivedEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter)
            where TDerivedEntity : TEntity;
    }
}