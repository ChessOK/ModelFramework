using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    public abstract class CommandInvokedHandler<T> : ApplicationBusMessageHandler<ICommandInvokedMessage<T>>
    {
        protected abstract void Handle(T command);

        protected sealed override void Handle(ICommandInvokedMessage<T> message)
        {
            Handle(message.Command);
        }
    }
}