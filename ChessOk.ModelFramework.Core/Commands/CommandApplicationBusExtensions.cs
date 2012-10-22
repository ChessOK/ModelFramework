using System;

using ChessOk.ModelFramework.Commands.Internals;

namespace ChessOk.ModelFramework.Commands
{
    public static class ApplicationBusCommandExtensions
    {
        public static TCommand BindAndInvoke<TCommand>(this IApplicationBus bus, ICommandParametersBinder<TCommand> binder)
            where TCommand : CommandBase
        {
            return bus.Invoke<TCommand>(binder.Bind);
        }

        public static TCommand BindAndInvoke<TCommand>(this IApplicationBus bus, 
            ICommandParametersBinder<TCommand> binder, Action<TCommand> initialization)
            where TCommand : CommandBase
        {
            return bus.Invoke<TCommand>(c =>
            {
                binder.Bind(c);
                initialization(c);
            });
        }

        public static SaveCommand<TEntity> InvokeSaveCommand<TEntity>(this IApplicationBus bus, TEntity entity)
            where TEntity : Entity
        {
            return bus.Invoke<SaveCommand<TEntity>>(x => x.Entity = entity);
        }

        public static DeleteCommand<TEntity> InvokeDeleteCommand<TEntity>(this IApplicationBus bus, TEntity entity)
            where TEntity : Entity
        {
            return bus.Invoke<DeleteCommand<TEntity>>(x => x.Entity = entity);
        }

        public static TCommand Invoke<TCommand>(this IApplicationBus bus)
            where TCommand : CommandBase
        {
            return bus.Invoke<TCommand>(null);
        }

        public static TCommand Invoke<TCommand>(this IApplicationBus bus, Action<TCommand> initialization)
            where TCommand : CommandBase
        {
            var command = bus.CreateCommand<TCommand>(initialization);
            bus.Invoke(command);

            return command;
        }

        public static void Invoke(this IApplicationBus bus, CommandBase command)
        {
            bus.Handle(command);
        }

        private static T CreateCommand<T>(this IApplicationBus bus, Action<T> initialization)
            where T : CommandBase
        {
            var command = bus.Context.Get<T>();
            if (initialization != null)
            {
                initialization(command);
            }

            return command;
        }
    }
}
