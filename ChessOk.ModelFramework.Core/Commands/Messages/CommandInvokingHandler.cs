using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    public abstract class CommandInvokingHandler<T> : ApplicationEventHandler<ICommandInvokingMessage<T>>
        where T : CommandBase
    {
        protected abstract void Handle(T command, out bool cancelInvocation);

        protected sealed override bool Handle(ICommandInvokingMessage<T> message)
        {
            bool cancelInvocation;
            Handle(message.Command, out cancelInvocation);

            if (cancelInvocation)
            {
                message.CancelInvocation();
            }
            return true;
        }
    }
}