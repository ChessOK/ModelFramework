using System;

namespace ChessOk.ModelFramework.Commands
{
    public class InlineCommand : Command
    {
        public InlineCommand(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Action = action;
        }

        public Action Action { get; private set; }

        protected override void Execute()
        {
            Action();
        }
    }
}
