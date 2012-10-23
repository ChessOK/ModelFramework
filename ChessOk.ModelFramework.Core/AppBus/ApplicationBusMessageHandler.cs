using System;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Messages
{
    public abstract class ApplicationBusMessageHandler : IApplicationBusMessageHandler
    {
        protected IApplicationBus Bus { get; private set; }

        protected IContext Context { get { return Bus.Context; } }
        protected IValidationContext Validation { get { return Bus.ValidationContext; } }

        public abstract void Handle(IApplicationBusMessage ev);

        internal void Bind(IApplicationBus bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException("bus");
            }

            Bus = bus;
        }
    }
}