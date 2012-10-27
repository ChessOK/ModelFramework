using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Scopes;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Messages
{
    public abstract class ApplicationBusMessageHandler : IApplicationBusMessageHandler
    {
        protected IApplicationBus Bus { get; private set; }

        protected IModelScope Model { get { return Bus.Model; } }
        protected IValidationContext Validation { get { return Bus.ValidationContext; } }

        public abstract void Handle(IApplicationBusMessage ev);

        public abstract IEnumerable<string> MessageNames { get; }

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