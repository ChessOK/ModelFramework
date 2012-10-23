using System;
using System.Linq;

using ChessOk.ModelFramework.Commands.Filters;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands.Internals
{
    public class CommandDispatcher : ApplicationBusMessageHandler<CommandBase>, ICommandDispatcher
    {
        private readonly IApplicationBus _bus;

        public CommandDispatcher(IApplicationBus bus)
        {
            _bus = bus;
        }

        protected override void Handle(CommandBase command)
        {
            // Привязка незамороженной команды к контексту
            command.Bind(_bus);

            // Создание и инициирование события о начале вызова команды с точным типом самой команды
            var invokingEvent = (ICommandInvokingMessage<object>)Activator.CreateInstance(
                typeof(CommandInvokingMessage<>).MakeGenericType(command.GetType()), command);

            _bus.Send(invokingEvent);

            // Если после вызова всех обработчиков флаг включен, то отменяем выполнение команды
            if (invokingEvent.InvocationCancelled) { return; }

            var filters = GetCommandFilters(command);

            if (filters.Length == 0)
            {
                command.Invoke();
            }
            else
            {
                var filterContext = new CommandFilterContext(_bus, command);

                Action commandAction = command.Invoke;
                for (var i = filters.Length - 1; i >= 0; i--)
                {
                    var innerAction = commandAction;
                    var filterIndex = i;
                    commandAction = () => filters[filterIndex].OnInvoke(filterContext, innerAction);
                }

                commandAction();
            }

            // Создание и инициирование события об успешном вызове команды с точным типом самой команды
            var invokedEvent = (ICommandInvokedMessage<object>)Activator.CreateInstance(
                typeof(CommandInvokedMessage<>).MakeGenericType(command.GetType()), command);

            _bus.Send(invokedEvent);
        }

        private static CommandFilterAttribute[] GetCommandFilters(CommandBase command)
        {
            return command.GetType().GetCustomAttributes(typeof(CommandFilterAttribute), true)
                .Cast<CommandFilterAttribute>()
                .OrderBy(x => x.Order)
                .ToArray();
        }
    }
}
