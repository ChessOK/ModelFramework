using System;

using ChessOk.ModelFramework.AsyncCommands.Messages;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Internals
{
    public class AsyncCommandDispatcher : ApplicationEventHandler<AsyncCommandWrapperMessage>
    {
        private readonly IApplicationBus _bus;

        public AsyncCommandDispatcher(IApplicationBus bus)
        {
            _bus = bus;
        }

        protected override bool Handle(AsyncCommandWrapperMessage asyncCommand)
        {
            var sendingEvent = (IAsyncCommandSendingMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandSendingMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Handle(sendingEvent);

            if (sendingEvent.SendingCancelled) return true;

            using (var queue = _bus.Context.Get<IAsyncCommandQueue>())
            {
                queue.Enqueue(asyncCommand.Command);
            }

            var sentEvent = (IAsyncCommandSentMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandSentMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Handle(sentEvent);

            return true;
        }
    }
}
