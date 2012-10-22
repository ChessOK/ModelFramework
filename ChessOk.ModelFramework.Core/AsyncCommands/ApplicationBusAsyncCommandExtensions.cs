using System;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.AsyncCommands
{
    public static class ApplicationBusAsyncCommandExtensions
    {
        public static void Send<T>(this IApplicationBus bus)
            where T : CommandBase
        {
            bus.Send<T>(null);
        }

        public static void Send<T>(this IApplicationBus bus, Action<T> initialization)
            where T : CommandBase
        {
            var command = bus.Create(initialization);
;
            bus.Send(command);
        }

        public static void Send(this IApplicationBus bus, CommandBase command)
        {
            bus.Handle(new AsyncCommandWrapperMessage(command));
        }

        private static T Create<T>(this IApplicationBus bus, Action<T> initialization)
            where T : CommandBase
        {
            var message = bus.Context.Get<T>();
            if (initialization != null)
            {
                initialization(message);
            }

            return message;
        }
    }
}
