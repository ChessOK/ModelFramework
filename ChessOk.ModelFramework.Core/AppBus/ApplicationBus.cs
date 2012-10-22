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
        private readonly ICollection<IApplicationEventHandler> _registeredEventHandlers =
            new Collection<IApplicationEventHandler>();

        private readonly IValidationContext _validationContext;
        private readonly IContext _context;

        public ApplicationBus(IContext parentContext)
        {
            _context = new Context(parentContext, ContextHierarchy.ApplicationBus, 
                x => x.RegisterInstance(this).As<IApplicationBus>().AsSelf());

            _validationContext = _context.Get<IValidationContext>();

            var handlers = _context.GetAll<IApplicationEventHandler>();
            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public IContext Context { get { return _context; } }
        public IValidationContext Validation { get { return _validationContext; } }

        /// <summary>
        /// Инициировать событие и вызвать все его обработчики.
        /// <para>
        /// Порядок обработки события не детерминирован. 
        /// Всем обработчикам передается один и тот же экземпляр события.
        /// </para>
        /// </summary>
        /// <returns>Число вызванных обработчиков</returns>
        /// <param name="message">Экземпляр события</param>
        public void Handle(IApplicationMessage message)
        {
            Validation.AssertObject(message).IsValid();
            Validation.ThrowExceptionIfInvalid();

            foreach (var handler in _registeredEventHandlers)
            {
                handler.Handle(message);
            }
        }

        public bool TryHandle(IApplicationMessage message)
        {
            try
            {
                Handle(message);
                return true;
            }
            catch (ValidationErrorsException)
            {
                return false;
            }
        }

        internal void RegisterHandler(IApplicationEventHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            _registeredEventHandlers.Add(handler);
            var appEventHandler = handler as ApplicationEventHandler;
            if (appEventHandler != null)
            {
                appEventHandler.Bind(this);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}