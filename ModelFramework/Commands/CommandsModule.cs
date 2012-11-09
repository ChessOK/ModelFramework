using Autofac;

using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.Commands
{
    /// <summary>
    /// Регистрирует основные классы для обработки команд.
    /// </summary>
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<ICommandDispatcher>())
                .As<IApplicationBusMessageHandler>();

            builder.Register(x => new CommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<ICommandDispatcher>();
        }
    }
}
