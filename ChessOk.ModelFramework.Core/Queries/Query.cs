namespace ChessOk.ModelFramework.Queries.Internals
{
    public abstract class Query
    {
        protected IModelContext Context { get; private set; }

        internal abstract void Invoke();

        internal void Bind(IModelContext model)
        {
            Context = model;
        }
    }
}