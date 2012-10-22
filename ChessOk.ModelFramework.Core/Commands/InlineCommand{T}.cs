using System;

namespace ChessOk.ModelFramework.Commands
{
    public class InlineCommand<T> : Command<T>
    {
        public InlineCommand(Func<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action = action;
        }

        public Func<T> Action { get; private set; } 

        protected override T Execute()
        {
            return Action();
        }
    }
}