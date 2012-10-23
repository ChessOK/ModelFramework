using System;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework
{
    public static class ApplicationBusExtensions
    {
        #region TrySend-related

        public static T TrySend<T>(this IApplicationBus bus)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(null);
        }

        public static T TrySend<T>(this IApplicationBus bus, Action<T> initialization)
            where T : IApplicationBusMessage
        {
            var message = bus.Context.Get<T>();
            if (initialization != null)
            {
                initialization(message);
            }

            bus.TrySend(message);

            return message;
        }

        public static T BindAndTrySend<T>(this IApplicationBus bus, IMessageParametersBinder<T> binder)
            where T : IApplicationBusMessage
        {
            return bus.TrySend<T>(binder.Bind);
        }

        public static T BindAndTrySend<T>(this IApplicationBus bus,
            IMessageParametersBinder<T> binder, Action<T> initialization)
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
            var message = bus.Context.Get<T>();
            if (initialization != null)
            {
                initialization(message);
            }

            bus.Send(message);

            return message;
        }

        public static T BindAndSend<T>(this IApplicationBus bus, IMessageParametersBinder<T> binder)
            where T : IApplicationBusMessage
        {
            if (binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            return bus.Send<T>(binder.Bind);
        }

        public static T BindAndSend<T>(this IApplicationBus bus,
            IMessageParametersBinder<T> binder, Action<T> initialization)
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
