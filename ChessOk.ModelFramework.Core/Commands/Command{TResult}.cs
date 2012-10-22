using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.Commands
{
    public abstract class Command<TResult> : CommandBase
    {
        public TResult Result { get; private set; }

        public sealed override void Invoke()
        {
            Result = Execute();
        }

        protected abstract TResult Execute();
    }
}
