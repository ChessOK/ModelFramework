using System.Collections.Generic;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands.Messages
{
    /// <summary>
    /// Обработчик сообщения <see cref="IAsyncCommandEnqueuedMessage{T}"/>, которое
    /// посылается в шину после добавления команды с типом <typeparamref name="T"/> 
    /// (наследник <see cref="CommandBase"/>) в очередь асинхронных команд.
    /// </summary>
    /// 
    /// <remarks>
    /// Так как в сообщении <see cref="IAsyncCommandEnqueuedMessage{T}"/> параметр <typeparamref name="T"/>
    /// объявлен ковариантным, то обработчик будет вызван после добавления в очередь всех команд, 
    /// являющихся наследниками типа <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавленной в очередь команды, наследник <see cref="CommandBase"/></typeparam>
    public abstract class AsyncCommandEnqueuedHandler<T> : ApplicationBusMessageHandler
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return AsyncCommandEnqueuedMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T message);

        public sealed override void Handle(IApplicationBusMessage ev)
        {
            var sentEvent = ev as IAsyncCommandEnqueuedMessage<T>;
            if (sentEvent == null)
            {
                return;
            }

            Handle(sentEvent.Command);
        }
    }
}
