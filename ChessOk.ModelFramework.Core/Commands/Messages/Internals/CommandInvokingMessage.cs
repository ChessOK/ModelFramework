namespace ChessOk.ModelFramework.Commands.Messages
{
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