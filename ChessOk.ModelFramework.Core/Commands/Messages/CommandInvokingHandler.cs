using System.Collections.Generic;

using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    public abstract class CommandInvokingHandler<T> : ApplicationBusMessageHandler<ICommandInvokingMessage<T>>
        where T : CommandBase
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return CommandInvokingMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T command, out bool cancelInvocation);

        protected sealed override void Handle(ICommandInvokingMessage<T> message)
        {
            bool cancelInvocation;
            Handle(message.Command, out cancelInvocation);

            if (cancelInvocation)
            {
                message.CancelInvocation();
            }
        }
    }
}