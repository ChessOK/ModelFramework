using System;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Commands.Internals
{
    public abstract class CommandBase : IApplicationBusMessage
    {
        protected IApplicationBus Bus { get; private set; }

        protected IContext Context { get { return Bus.Context; } }
        protected IValidationContext Validation { get { return Bus.ValidationContext; } }

        internal void Bind(IApplicationBus bus)
        {
            if (Bus != null)
            {
                throw new InvalidOperationException("Command is already bound to a bus");
            }

            if (bus == null)
            {
                throw new ArgumentNullException("bus");
            }

            Bus = bus;
        }

        public abstract void Invoke();

        public string MessageName
        {
            get { return GetMessageName(); }
        }

        public static string GetMessageName()
        {
            return typeof(CommandBase).Name;
        }
    }
}