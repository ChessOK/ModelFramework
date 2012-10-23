using System;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    public abstract class AsyncCommandSendingHandler<T> : ApplicationBusMessageHandler
    {
        protected abstract void Handle(T message, out bool cancelSending);

        public override void Handle(IApplicationBusMessage ev)
        {
            var sendingEvent = ev as IAsyncCommandSendingMessage<T>;
            if (sendingEvent == null)
            {
                return;
            }

            bool cancelSending;
            Handle(sendingEvent.Command, out cancelSending);

            if (cancelSending)
            {
                sendingEvent.CancelSending();
            }
        }
    }
}
