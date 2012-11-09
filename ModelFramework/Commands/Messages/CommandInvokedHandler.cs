using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// Обработчик сообщения <see cref="ICommandInvokedMessage{T}"/>, которое
    /// посылается в шину после выполнения команды с типом <typeparamref name="T"/> 
    /// (наследник <see cref="CommandBase"/>).
    /// </summary>
    /// 
    /// <remarks>
    /// Так как в сообщении <see cref="ICommandInvokedMessage{T}"/> параметр <typeparamref name="T"/>
    /// объявлен ковариантным, то обработчик будет вызван после выполнения всех команд, 
    /// являющихся наследниками типа <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">Тип выполненной команды, наследник <see cref="CommandBase"/></typeparam>
    public abstract class CommandInvokedHandler<T> : ApplicationBusMessageHandler<ICommandInvokedMessage<T>>
    {
        public sealed override IEnumerable<string> MessageNames
        {
            get { yield return CommandInvokedMessage<object>.GetMessageName(); }
        }

        protected abstract void Handle(T command);

        protected sealed override void Handle(ICommandInvokedMessage<T> message)
        {
            Handle(message.Command);
        }
    }
}