using ChessOk.ModelFramework.Queries.Internals;

namespace ChessOk.ModelFramework.Queries
{
    public abstract class Query<TResult> : Query
    {
        public TResult Result { get; private set; }

        internal override void Invoke()
        {
            Result = Execute();
        }

        protected abstract TResult Execute();
    }
}
