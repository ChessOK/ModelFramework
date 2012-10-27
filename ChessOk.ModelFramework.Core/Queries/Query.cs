using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework.Queries.Internals
{
    public abstract class Query
    {
        protected IModelScope Model { get; private set; }

        internal abstract void Invoke();

        internal void Bind(IModelScope modelScope)
        {
            Model = modelScope;
        }
    }
}