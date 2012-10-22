using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    public abstract class CommandInvokedHandler<T> : ApplicationEventHandler<ICommandInvokedMessage<T>>
    {
        protected abstract void Handle(T command);

        protected sealed override bool Handle(ICommandInvokedMessage<T> message)
        {
            Handle(message.Command);
            return true;
        }
    }
}