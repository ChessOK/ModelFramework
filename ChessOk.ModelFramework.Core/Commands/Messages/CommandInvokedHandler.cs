using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    public abstract class CommandInvokedHandler<T> : ApplicationBusMessageHandler<ICommandInvokedMessage<T>>
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return CommandInvokedMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T command);

        protected sealed override void Handle(ICommandInvokedMessage<T> message)
        {
            Handle(message.Command);
        }
    }
}