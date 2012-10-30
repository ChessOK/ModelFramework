namespace ChessOk.ModelFramework.Queries.Internals
{
    public abstract class Query
    {
        protected IModelContext Model { get; private set; }

        internal abstract void Invoke();

        internal void Bind(IModelContext model)
        {
            Model = model;
        }
    }
}