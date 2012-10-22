using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework
{
    public interface ICommandParametersBinder<in T>
        where T : CommandBase
    {
        void Bind(T command);
    }
}
