using System;
using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Реализует интерфейс <see cref="IApplicationBus"/>. 
    /// </summary>
    /// 
    /// <remarks>
    /// Список обработчиков
    /// получает при инициализации, путем запроса коллекции зарегистрированных
    /// в контейнере типов <see cref="IApplicationBusMessageHandler"/>.
    /// 
    /// Для обработчиков типа <see cref="ApplicationBusMessageHandler"/> автоматически
    /// предоставляет свой экземпляр <see cref="ApplicationBus"/> при инициализации.
    /// 
    /// Создает дочерний <see cref="IModelContext"/>, предоставляющий
    /// <see cref="ILifetimeScope"/> с тегом <see cref="ContextHierarchy.ApplicationBus"/>.
    /// 
    /// Создает и управляет <see cref="ValidationContext"/>, который является
    /// общим для экземпляра <see cref="ApplicationBus"/> и всех сообщений <see cref="IApplicationBusMessage"/>.
    /// </remarks>
    public class ApplicationBus : IApplicationBus
    {
        private readonly IDictionary<string, IList<IApplicationBusMessageHandler>> _subscriptions =
            new Dictionary<string, IList<IApplicationBusMessageHandler>>();

        private readonly IValidationContext _validationContext;
        private readonly IModelContext _context;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ApplicationBus"/>, используя
        /// в качестве родительского контекста <paramref name="parentModelContext"/>.
        /// </summary>
        /// <param name="parentModelContext"></param>
        public ApplicationBus(
            IModelContext parentModelContext, 
            IEnumerable<IApplicationBusMessageHandler> handlers)
        {
            if (parentModelContext == null)
            {
                throw new ArgumentNullException("parentModelContext");
            }

            _context = parentModelContext.CreateChildContext(
                ContextHierarchy.ApplicationBus, 
                x => x.RegisterInstance(this).As<IApplicationBus>().AsSelf());

            _validationContext = _context.Get<IValidationContext>();

            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public IModelContext Context { get { return _context; } }
        public IValidationContext ValidationContext { get { return _validationContext; } }

        public void Send(IApplicationBusMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            ValidationContext.Ensure(message).IsValid();
            ValidationContext.ThrowExceptionIfInvalid();

            var messageName = message.MessageName;

            if (String.IsNullOrEmpty(messageName))
            {
                throw new InvalidOperationException(
                    String.Format("Message name can not be null or empty in class {0}", message.GetType().Name));
            }

            if (_subscriptions.ContainsKey(messageName))
            {
                var handlers = _subscriptions[messageName];
                foreach (var handler in handlers)
                {
                    handler.Handle(message);
                }
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

            var appEventHandler = handler as ApplicationBusMessageHandler;
            if (appEventHandler != null)
            {
                appEventHandler.Bind(this);
            }

            var messageNames = handler.MessageNames;

            if (messageNames == null)
            {
                throw new InvalidOperationException("Handler's MessageNames could not be null");
            }

            foreach (var messageName in handler.MessageNames)
            {
                IList<IApplicationBusMessageHandler> handlers;
                if (_subscriptions.ContainsKey(messageName))
                {
                    handlers = _subscriptions[messageName];
                }
                else
                {
                    handlers = new List<IApplicationBusMessageHandler>();
                    _subscriptions.Add(messageName, handlers);
                }

                handlers.Add(handler);
            }
        }

        public void Dispose()
        {
            _validationContext.Dispose();
            _context.Dispose();
        }
    }
}