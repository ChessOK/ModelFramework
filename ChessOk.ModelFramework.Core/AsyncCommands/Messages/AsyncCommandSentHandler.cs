using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    public abstract class AsyncCommandSentHandler<T> : ApplicationEventHandler
    {
        protected abstract void Handle(T message);

        public sealed override bool Handle(IApplicationMessage ev)
        {
            var sentEvent = ev as IAsyncCommandSentMessage<T>;
            if (sentEvent == null)
            {
                return false;
            }

            Handle(sentEvent.Command);
            return true;
        }
    }
}
