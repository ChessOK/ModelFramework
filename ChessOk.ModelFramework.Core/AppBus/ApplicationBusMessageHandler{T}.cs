using System;

namespace ChessOk.ModelFramework.Messages
{
    public abstract class ApplicationBusMessageHandler<T> : ApplicationBusMessageHandler
        where T : class, IApplicationBusMessage
    {
        protected abstract void Handle(T message);

        public sealed override void Handle(IApplicationBusMessage ev)
        {
            var casted = ev as T;
            if (casted == null)
            {
                return;
            }

            Handle(casted);
        }
    }
}