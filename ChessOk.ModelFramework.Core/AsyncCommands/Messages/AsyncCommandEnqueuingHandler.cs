using System.Collections.Generic;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// Обработчик сообщения <see cref="IAsyncCommandEnqueuingMessage{T}"/>, которое
    /// посылается в шину перед добавлением команды с типом <typeparamref name="T"/>
    /// (наследник <see cref="CommandBase"/>) в очередь асинхронных команд.
    /// </summary>
    /// 
    /// <remarks>
    /// Так как в сообщении <see cref="IAsyncCommandEnqueuingMessage{T}"/> параметр <typeparamref name="T"/>
    /// объявлен ковариантным, то обработчик будет вызван перед добавлением в очередь всех команд, 
    /// являющихся наследниками типа <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавляемой в очередь команды, наследник <see cref="CommandBase"/></typeparam>
    public abstract class AsyncCommandEnqueuingHandler<T> : ApplicationBusMessageHandler
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return AsyncCommandEnqueuingMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T message, out bool cancelEnqueuing);

        public override void Handle(IApplicationBusMessage ev)
        {
            var sendingEvent = ev as IAsyncCommandEnqueuingMessage<T>;
            if (sendingEvent == null)
            {
                return;
            }

            bool cancelEnqueuing;
            Handle(sendingEvent.Command, out cancelEnqueuing);

            if (cancelEnqueuing)
            {
                sendingEvent.CancelEnqueuing();
            }
        }
    }
}
