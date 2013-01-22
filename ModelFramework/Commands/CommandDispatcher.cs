using System;
using System.Collections.Generic;
using System.Linq;

using ChessOk.ModelFramework.Commands.Filters;
using ChessOk.ModelFramework.Commands.Messages;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Обработчик сообщений типа <see cref="CommandBase"/>, выполняет команды
    /// в синхронном режиме.
    /// </summary>
    /// 
    /// <remarks>
    /// Перед выполнением команды к ней применяются фильтры (см. <see cref="CommandFilterAttribute"/>).
    /// 
    /// При обработке сообщения <see cref="CommandBase"/> в шину сообщений будут
    /// отправлены дополнительные сообщения.
    /// 
    /// <para>
    /// Перед выполнением команды в шину приложения отправляется
    /// сообщение <see cref="ICommandInvokingMessage{T}"/>, которое
    /// можно обработать, используя зарегистрированный в контейнере
    /// <see cref="CommandInvokingHandler{T}"/>, где T — тип выполняемой команды.
    /// Внутри обработчика можно отменить выполнение команды.
    /// </para>
    /// 
    /// <para>
    /// После успешного выполнения команды (если оно не было отменено), 
    /// в шину отправляется сообшение <see cref="ICommandInvokedMessage{T}"/>,
    /// которое можно обработать, используя зарегистрированный в контейнере
    /// <see cref="CommandInvokedHandler{T}"/>, где T — тип выполненной команды.
    /// </para>
    /// </remarks>
    public class CommandDispatcher : ApplicationBusMessageHandler<CommandBase>, ICommandDispatcher
    {
        public override IEnumerable<string> MessageNames
        {
            get { yield return CommandBase.GetMessageName(); }
        }

        protected override void Handle(CommandBase command)
        {
            command.Bind(Bus);

            var invokingEvent = (ICommandInvokingMessage<object>)Activator.CreateInstance(
                typeof(CommandInvokingMessage<>).MakeGenericType(command.GetType()), command);

            Bus.Send(invokingEvent);

            if (invokingEvent.InvocationCancelled) { return; }

            var filters = GetCommandFilters(command);

            if (filters.Length == 0)
            {
                command.Invoke();
            }
            else
            {
                var filterContext = new CommandFilterContext(Bus, command);

                Action commandAction = command.Invoke;
                for (var i = filters.Length - 1; i >= 0; i--)
                {
                    var innerAction = commandAction;
                    var filterIndex = i;
                    commandAction = () => filters[filterIndex].Apply(filterContext, innerAction);
                }

                commandAction();
            }

            command.RaiseInvoked();

            var invokedEvent = (ICommandInvokedMessage<object>)Activator.CreateInstance(
                typeof(CommandInvokedMessage<>).MakeGenericType(command.GetType()), command);

            Bus.Send(invokedEvent);
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
