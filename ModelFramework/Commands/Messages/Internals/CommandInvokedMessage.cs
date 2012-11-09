namespace ChessOk.ModelFramework.Commands.Messages
{
    internal class CommandInvokedMessage<T> : ICommandInvokedMessage<T>
    {
        public CommandInvokedMessage(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(CommandInvokedMessage<>).Name;
        }
    }
}