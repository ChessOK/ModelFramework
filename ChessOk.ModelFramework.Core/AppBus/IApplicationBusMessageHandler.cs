namespace ChessOk.ModelFramework.Messages
{
    public interface IApplicationBusMessageHandler
    {
        void Handle(IApplicationBusMessage ev);
    }
}
