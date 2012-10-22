using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Handlers
{
    public interface IAsyncCommandHandler
    {
        void Handle(CommandBase asyncCommand);
    }
}