using System;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Messages
{
    public abstract class ApplicationEventHandler : IApplicationEventHandler
    {
        protected ApplicationBus Bus { get; private set; }

        protected IContext Context { get { return Bus.Context; } }
        protected IValidationContext Validation { get { return Bus.Validation; } }

        public abstract bool Handle(IApplicationMessage ev);

        internal void Bind(ApplicationBus bus)
        {
            if (Bus != null)
            {
                throw new InvalidOperationException("Handler is already bound to a bus");
            }

            if (bus == null)
            {
                throw new ArgumentNullException("bus");
            }

            Bus = bus;
        }
    }
}