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
        #region TrySend-related

        public static bool TrySend<T>(this IApplicationBus bus)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null, null);
        }

        public static bool TrySend<T>(this IApplicationBus bus, out T message)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null, null, out message);
        }

        public static bool TrySend<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null, initialization);
        }

        public static bool TrySend<T>(this IApplicationBus bus, Action<T> initialization, out T message)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null, initialization, out message);
        }

        public static bool TrySend<T>(this IApplicationBus bus, IMessageBinder<T> binder)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(binder, null);
        }

        public static bool TrySend<T>(this IApplicationBus bus, IMessageBinder<T> binder, out T message)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(binder, null, out message);
        }

        public static bool TrySend<T>(this IApplicationBus bus, IMessageBinder<T> binder, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            T message;
            return bus.TrySend<T>(binder, initialization, out message);
        }

        public static bool TrySend<T>(this IApplicationBus bus, IMessageBinder<T> binder, Action<T> initialization, out T message)
            where T : IApplicationBusMessage
        {
            message = bus.Context.Get<T>();
            if (binder != null)
            {
                binder.Bind(message);
            }

            if (initialization != null)
            {
                initialization(message);
            }

            return bus.TrySend(message);
        }

        #endregion

        #region Send-related

        public static void Send<T>(this IApplicationBus bus)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(null, null);
        }

        public static void Send<T>(this IApplicationBus bus, out T message)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(null, null, out message);
        }

        public static void Send<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(null, initialization);
        }

        public static void Send<T>(this IApplicationBus bus, Action<T> initialization, out T message)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(null, initialization, out message);
        }

        public static void Send<T>(this IApplicationBus bus, IMessageBinder<T> binder)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(binder, null);
        }

        public static void Send<T>(this IApplicationBus bus, IMessageBinder<T> binder, out T message)
            where T : IApplicationBusMessage
        {
            bus.Send<T>(binder, null, out message);
        }

        public static void Send<T>(this IApplicationBus bus,
            IMessageBinder<T> binder, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            T message;
            bus.Send<T>(binder, initialization, out message);
        }

        public static void Send<T>(this IApplicationBus bus, IMessageBinder<T> binder, Action<T> initialization, out T message)
            where T : IApplicationBusMessage
        {
            message = bus.Context.Get<T>();
            if (binder != null)
            {
                binder.Bind(message);
            }

            if (initialization != null)
            {
                initialization(message);
            }

            bus.Send(message);
        }

        #endregion
    }
}
