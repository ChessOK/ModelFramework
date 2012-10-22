using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.Commands
{
    public abstract class Command : CommandBase
    {
        public sealed override void Invoke()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}