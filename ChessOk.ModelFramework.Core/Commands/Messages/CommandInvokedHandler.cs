using System.Collections.Generic;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// ќбработчик сообщени€ <see cref="ICommandInvokedMessage{T}"/>, которое
    /// посылаетс€ в шину после выполнени€ команды с типом <typeparamref name="T"/> 
    /// (наследник <see cref="CommandBase"/>).
    /// </summary>
    /// 
    /// <remarks>
    /// “ак как в сообщении <see cref="ICommandInvokedMessage{T}"/> параметр <typeparamref name="T"/>
    /// объ€влен ковариантным, то обработчик будет вызван после выполнени€ всех команд, 
    /// €вл€ющихс€ наследниками типа <typeparamref name="T"/>.
    /// </remarks>
    /// 
    /// <typeparam name="T">“ип выполненной команды, наследник <see cref="CommandBase"/></typeparam>
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