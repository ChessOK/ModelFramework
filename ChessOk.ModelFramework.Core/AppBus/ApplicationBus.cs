using System;
using System.Collections.Generic;

using Autofac;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// ��������� ��������� <see cref="IApplicationBus"/>. 
    /// </summary>
    /// 
    /// <remarks>
    /// ������ ������������
    /// �������� ��� �������������, ����� ������� ��������� ������������������
    /// � ���������� ����� <see cref="IApplicationBusMessageHandler"/>.
    /// 
    /// ��� ������������ ���� <see cref="ApplicationBusMessageHandler"/> �������������
    /// ������������� ���� ��������� <see cref="ApplicationBus"/> ��� �������������.
    /// 
    /// ������� �������� <see cref="IModelContext"/>, ���������������
    /// <see cref="ILifetimeScope"/> � ����� <see cref="ContextHierarchy.ApplicationBus"/>.
    /// 
    /// ������� � ��������� <see cref="ValidationContext"/>, ������� ��������
    /// ����� ��� ���������� <see cref="ApplicationBus"/> � ���� ��������� <see cref="IApplicationBusMessage"/>.
    /// </remarks>
    public class ApplicationBus : IApplicationBus
    {
        private readonly IDictionary<string, IList<IApplicationBusMessageHandler>> _subscriptions =
            new Dictionary<string, IList<IApplicationBusMessageHandler>>();

        private readonly IValidationContext _validationContext;
        private readonly IModelContext _model;

        /// <summary>
        /// �������������� ��������� ������ <see cref="ApplicationBus"/>, ���������
        /// � �������� ������������� ��������� <paramref name="parentModelContext"/>.
        /// </summary>
        /// <param name="parentModelContext"></param>
        public ApplicationBus(IModelContext parentModelContext)
        {
            if (parentModelContext == null)
            {
                throw new ArgumentNullException("parentModelContext");
            }

            _model = parentModelContext.CreateChildContext(
                ContextHierarchy.ApplicationBus, 
                x => x.RegisterInstance(this).As<IApplicationBus>().AsSelf());

            _validationContext = _model.Get<IValidationContext>();

            var handlers = _model.GetAll<IApplicationBusMessageHandler>();
            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public IModelContext Model { get { return _model; } }
        public IValidationContext ValidationContext { get { return _validationContext; } }

        public void Send(IApplicationBusMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            ValidationContext.Ensure(message).IsValid(usePropertyNamesAsErrorKeys: false);
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
            _model.Dispose();
        }
    }
}