namespace ChessOk.ModelFramework.Commands
{
    public class DeleteCommand<TEntity> : Command
        where TEntity : Entity
    {
        public TEntity Entity { get; set; }

        protected override void Execute()
        {
            Context.GetRepository<TEntity>().Delete(Entity);
        }
    }
}
