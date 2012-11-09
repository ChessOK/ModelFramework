using System;

using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет основные расширения шины приложения <see cref="IApplicationBus"/>.
    /// </summary>
    public static class ApplicationBusExtensions
    {
        /// <summary>
        /// Возвращает значение, нет ли валидационных ошибок в текущем 
        /// экземпляре <see cref="IValidationContext"/>.
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public static bool IsValid(this IApplicationBus bus)
        {
            return bus.ValidationContext.IsValid;
        }

        /// <summary>
        /// Получает зарегистрированный в контейнере экземпляр типа <typeparamref name="T"/>,
        /// проинициализированный с помощью <paramref name="initialization"/>.
        /// </summary>
        /// <typeparam name="T">Тип получаемого сообщения.</typeparam>
        /// <param name="bus"></param>
        /// <param name="initialization">Инициализация экземпляра.</param>
        /// <returns>Экземпляр сообщения.</returns>
        public static T Create<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            var message = bus.Context.Get<T>();
            if (initialization != null)
            {
                initialization(message);
            }

            return message;
        }

        #region TrySend-related

        public static T TrySend<T>(this IApplicationBus bus)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null);
        }

        public static T TrySend<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            var message = bus.Create<T>(initialization);

            bus.TrySend(message);

            return message;
        }

        public static T BindAndTrySend<T>(this IApplicationBus bus, IMessageBinder<T> binder)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(binder.Bind);
        }

        public static T BindAndTrySend<T>(this IApplicationBus bus,
            IMessageBinder<T> binder, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(c =>
            {
                binder.Bind(c);
                initialization(c);
            });
        }

        #endregion

        #region Send-related

        public static T Send<T>(this IApplicationBus bus)
            where T : IApplicationBusMessage
        {
            return bus.Send<T>(null);
        }

        public static T Send<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            var message = bus.Create(initialization);

            bus.Send(message);

            return message;
        }

        public static T BindAndSend<T>(this IApplicationBus bus, IMessageBinder<T> binder)
            where T : IApplicationBusMessage
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            return bus.Send<T>(binder.Bind);
        }

        public static T BindAndSend<T>(this IApplicationBus bus,
            IMessageBinder<T> binder, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            return bus.Send<T>(c =>
            {
                binder.Bind(c);
                initialization(c);
            });
        }

        #endregion
    }
}
