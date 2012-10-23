using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// —обытие инициируетс€ ѕќ—Ћ≈ вызова команды указанного типа или его наследника.
    /// </summary>
    /// <typeparam name="T">“ип вызванной команды.</typeparam>
    public interface ICommandInvokedMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Ёкземпл€р вызванной команды.
        /// </summary>
        T Command { get; }
    }

    internal class CommandInvokedMessage<T> : ICommandInvokedMessage<T>
    {
        public CommandInvokedMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
    }
}