using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Commands
{
    public class SaveCommand<TEntity> : Command<TEntity>
        where TEntity : Entity
    {
        [ValidatePresence, ValidateObject]
        public TEntity Entity { get; set; }

        protected override TEntity Execute()
        {
            Validation.ThrowExceptionIfInvalid();

            Context.GetRepository<TEntity>().Save(Entity);
            return Entity;
        }
    }
}
