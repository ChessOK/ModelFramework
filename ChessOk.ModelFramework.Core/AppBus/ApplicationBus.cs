using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Autofac;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    public class ApplicationBus : IApplicationBus
    {
        private readonly ICollection<IApplicationBusMessageHandler> _registeredEventHandlers =
            new Collection<IApplicationBusMessageHandler>();

        private readonly IValidationContext _validationContext;
        private readonly IContext _context;

        public ApplicationBus(IContext parentContext)
        {
            if (parentContext == null)
            {
                throw new ArgumentNullException("parentContext");
            }

            _context = new Context(parentContext, ContextHierarchy.ApplicationBus, 
                x => x.RegisterInstance(this).As<IApplicationBus>().AsSelf());

            _validationContext = _context.Get<IValidationContext>();

            var handlers = _context.GetAll<IApplicationBusMessageHandler>();
            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public IContext Context { get { return _context; } }
        public IValidationContext ValidationContext { get { return _validationContext; } }
        public IEnumerable<IApplicationBusMessageHandler> Handlers { get { return _registeredEventHandlers; } }

        public void Send(IApplicationBusMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            ValidationContext.Ensure(message).IsValid();
            ValidationContext.ThrowExceptionIfInvalid();

            foreach (var handler in _registeredEventHandlers)
            {
                handler.Handle(message);
            }
        }

        public bool TrySend(IApplicationBusMessage message)
        {
            try
            {
                Send(message);
                return true;
            }
            catch (ValidationException)
            {
                return false;
            }
        }

        internal void RegisterHandler(IApplicationBusMessageHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            _registeredEventHandlers.Add(handler);
            var appEventHandler = handler as ApplicationBusMessageHandler;
            if (appEventHandler != null)
            {
                appEventHandler.Bind(this);
            }
        }

        public void Dispose()
        {
            _validationContext.Dispose();
            _context.Dispose();
        }
    }
}