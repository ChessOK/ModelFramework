using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    public abstract class AsyncCommandSendingHandler<T> : ApplicationEventHandler
    {
        protected abstract void Handle(T message, out bool cancelSending);

        public override bool Handle(IApplicationMessage ev)
        {
            var sendingEvent = ev as IAsyncCommandSendingMessage<T>;
            if (sendingEvent == null)
            {
                return false;
            }

            bool cancelSending;
            Handle(sendingEvent.Command, out cancelSending);

            if (cancelSending)
            {
                sendingEvent.CancelSending();
            }
            return true;
        }
    }
}
