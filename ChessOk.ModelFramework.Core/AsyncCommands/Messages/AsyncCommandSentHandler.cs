using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    public abstract class AsyncCommandSentHandler<T> : ApplicationBusMessageHandler
    {
        protected abstract void Handle(T message);

        public sealed override void Handle(IApplicationBusMessage ev)
        {
            var sentEvent = ev as IAsyncCommandSentMessage<T>;
            if (sentEvent == null)
            {
                return;
            }

            Handle(sentEvent.Command);
        }
    }
}
