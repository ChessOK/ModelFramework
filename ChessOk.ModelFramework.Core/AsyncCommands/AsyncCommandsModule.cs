using Autofac;

using ChessOk.ModelFramework.AsyncCommands.Handlers;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.AsyncCommands.Workers;
using ChessOk.ModelFramework.Logging;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands
{
    /// <summary>
    /// Регистрирует основные классы для обработки асинхронных команд.
    /// </summary>
    public class AsyncCommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<IAsyncCommandDispatcher>())
                .As<IApplicationBusMessageHandler>();

            builder.Register(x => new AsyncCommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<IAsyncCommandDispatcher>();

            builder.Register(x => new SeparatedContextsHandler(x.Resolve<ILifetimeScope>(), x.Resolve<ILogger>()))
               .As<IAsyncCommandHandler>();

            builder.Register(x => new BackgroundThreadWorker(
                x.Resolve<IAsyncCommandQueue>(), 
                x.Resolve<IAsyncCommandHandler>(),
                x.Resolve<ILogger>()))
                .AsSelf();
        }
    }
}
