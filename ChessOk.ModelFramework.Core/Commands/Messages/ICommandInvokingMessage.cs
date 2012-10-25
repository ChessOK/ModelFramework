using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Messages
{
    /// <summary>
    /// <para>
    /// Событие инициируется ДО вызова команды указанного типа или его наследника. 
    /// </para>
    /// <para>
    /// Её выполнение можно отменить, выставив соответствующее значение
    /// в свойстве <see cref="InvocationCancelled"/>.
    /// </para>
    /// <typeparam name="T">Тип вызываемой команды.</typeparam>
    /// </summary>
    public interface ICommandInvokingMessage<out T> : IApplicationBusMessage
    {
        /// <summary>
        /// Экземпляр вызываемой команды.
        /// </summary>
        T Command { get; }

        /// <summary>
        /// Отменить выполнение команды. 
        /// <remarks>
        /// Чтобы команда была отменена, нужно, чтобы после вызова всех обработчиков, этот флаг сохранился.
        /// </remarks>
        /// </summary>
        bool InvocationCancelled { get; }
        void CancelInvocation();
    }

    internal class CommandInvokingMessage<T> : ICommandInvokingMessage<T>
    {
        public CommandInvokingMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
        public bool InvocationCancelled { get; private set; }

        public void CancelInvocation()
        {
            InvocationCancelled = true;
        }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(ICommandInvokingMessage<>).Name;
        }
    }
}