namespace ChessOk.ModelFramework.Queries.Internals
{
    public abstract class Query
    {
        protected ModelContext Context { get; private set; }

        internal abstract void Invoke();

        internal void Bind(ModelContext context)
        {
            Context = context;
        }
    }
}