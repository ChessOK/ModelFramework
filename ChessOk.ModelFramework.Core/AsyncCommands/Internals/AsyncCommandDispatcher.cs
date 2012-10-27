using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.AsyncCommands.Messages;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Internals
{
    public class AsyncCommandDispatcher : ApplicationBusMessageHandler<AsyncCommand>, IAsyncCommandDispatcher
    {
        private readonly IApplicationBus _bus;

        public AsyncCommandDispatcher(IApplicationBus bus)
        {
            _bus = bus;
        }

        protected override void Handle(AsyncCommand asyncCommand)
        {
            var sendingEvent = (IAsyncCommandSendingMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandSendingMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Send(sendingEvent);

            if (sendingEvent.SendingCancelled) { return; }

            using (var queue = _bus.Model.Get<IAsyncCommandQueue>())
            {
                queue.Enqueue(asyncCommand.Command);
            }

            var sentEvent = (IAsyncCommandSentMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandSentMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Send(sentEvent);
        }

        public override IEnumerable<string> MessageNames
        {
            get { yield return AsyncCommand.GetMessageName(); }
        }
    }
}
