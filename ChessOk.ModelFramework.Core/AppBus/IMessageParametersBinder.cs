using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework
{
    public interface IMessageParametersBinder<in T>
        where T : IApplicationBusMessage
    {
        void Bind(T message);
    }
}
