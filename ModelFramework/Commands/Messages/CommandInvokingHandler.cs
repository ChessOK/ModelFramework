using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// Обработчик сообщения <see cref="ICommandInvokingMessage{T}"/>, которое
    /// посылается в шину перед выполнением команды с типом <typeparamref name="T"/>
    /// (наследник <see cref="CommandBase"/>).
    /// </summary>
    /// 
    /// <remarks>
    /// Так как в сообщении <see cref="ICommandInvokingMessage{T}"/> параметр <typeparamref name="T"/>
    /// объявлен ковариантным, то обработчик будет вызван перед выполнением всех команд, 
    /// являющихся наследниками типа <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип добавляемой в очередь команды, наследник <see cref="CommandBase"/></typeparam>
    public abstract class CommandInvokingHandler<T> : ApplicationBusMessageHandler<ICommandInvokingMessage<T>>
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