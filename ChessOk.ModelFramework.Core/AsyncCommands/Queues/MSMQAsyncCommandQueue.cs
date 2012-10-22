using System;
using System.Messaging;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands.Queues
{
    public class MSMQAsyncCommandQueue : IAsyncCommandQueue
    {
        private readonly IMessageFormatter _formatter;
        private readonly MessageQueue _innerQueue;

        public MSMQAsyncCommandQueue(string queueName)
            : this(queueName, new BinaryMessageFormatter())
        {
        }

        public MSMQAsyncCommandQueue(string queueName, IMessageFormatter formatter)
        {
            _formatter = formatter;
            _innerQueue = MessageQueue.Exists(queueName) 
                ? new MessageQueue(queueName) 
                : MessageQueue.Create(queueName);
        }

        public void Enqueue(CommandBase asyncCommand)
        {
            if (asyncCommand == null)
            {
                throw new ArgumentNullException("asyncCommand");
            }

            var msg = new Message
                {
                    Body = asyncCommand, 
                    Label = asyncCommand.GetType().FullName, 
                    Formatter = _formatter
                };

            _innerQueue.Send(msg);
        }

        public CommandBase Dequeue()
        {
            try
            {
                var msg = _innerQueue.Receive(TimeSpan.FromSeconds(1));

                if (msg == null) return null;

                msg.Formatter = _formatter;
                return (CommandBase)msg.Body;
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                {
                    throw;
                }

                return null;
            }
        }

        public void Dispose()
        {
            _innerQueue.Dispose();
        }
    }
}
