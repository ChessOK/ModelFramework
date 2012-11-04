using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.AsyncCommands.Messages;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands
{
    /// <summary>
    /// Обработчик сообщений типа <see cref="AsyncCommand"/>. Помещает
    /// асинхронно выполняемую команду <see cref="CommandBase"/> в очередь
    /// <see cref="IAsyncCommandQueue"/> и возвращает управление программе,
    /// не обрабатывая саму команду.
    /// 
    /// <remarks>
    /// При обработке сообщения <see cref="AsyncCommand"/> в шину сообщений
    /// будут отправлены дополнительные сообщения.
    /// 
    /// <para>
    /// Перед добавлением команды в очередь в шину приложения отправляется
    /// сообщение <see cref="IAsyncCommandEnqueuingMessage{T}"/>, которое
    /// можно обработать, используя зарегистрированный в контейнере
    /// <see cref="AsyncCommandEnqueuingHandler{T}"/>, где T — тип добавляемой команды.
    /// Внутри обработчика можно отменить добавление команды в очередь.
    /// </para>
    /// 
    /// <para>
    /// После успешного добавления (если добавление не было отменено) команды в 
    /// очередь, в шину отправляется сообшение <see cref="IAsyncCommandEnqueuedMessage{T}"/>,
    /// которое можно обработать, используя зарегистрированный в контейнере
    /// <see cref="AsyncCommandEnqueuedHandler{T}"/>, где T — тип добавленной команды.
    /// </para>
    /// </remarks>
    /// </summary>
    public class AsyncCommandDispatcher : ApplicationBusMessageHandler<AsyncCommand>, IAsyncCommandDispatcher
    {
        private readonly IApplicationBus _bus;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AsyncCommandDispatcher"/>,
        /// используя <paramref name="bus"/>.
        /// </summary>
        /// <param name="bus">Шина приложения.</param>
        public AsyncCommandDispatcher(IApplicationBus bus)
        {
            _bus = bus;
        }

        protected override void Handle(AsyncCommand asyncCommand)
        {
            var sendingEvent = (IAsyncCommandEnqueuingMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandEnqueuingMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Send(sendingEvent);

            if (sendingEvent.EnqueuingCancelled) { return; }

            using (var queue = _bus.Context.Get<IAsyncCommandQueue>())
            {
                queue.Enqueue(asyncCommand.Command);
            }

            var sentEvent = (IAsyncCommandEnqueuedMessage<object>)Activator.CreateInstance(
                typeof(AsyncCommandEnqueuedMessage<>).MakeGenericType(asyncCommand.Command.GetType()), asyncCommand.Command);

            _bus.Send(sentEvent);
        }

        public override IEnumerable<string> MessageNames
        {
            get { yield return AsyncCommand.GetMessageName(); }
        }
    }
}
