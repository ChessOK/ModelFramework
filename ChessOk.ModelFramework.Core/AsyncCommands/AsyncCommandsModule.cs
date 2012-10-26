using Autofac;

using ChessOk.ModelFramework.AsyncCommands.Handlers;
using ChessOk.ModelFramework.AsyncCommands.Internals;
using ChessOk.ModelFramework.AsyncCommands.Queues;
using ChessOk.ModelFramework.AsyncCommands.Workers;
using ChessOk.ModelFramework.Messages;

namespace ChessOk.ModelFramework.AsyncCommands
{
    public class AsyncCommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<IAsyncCommandDispatcher>())
                .As<IApplicationBusMessageHandler>();

            builder.Register(x => new AsyncCommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<IAsyncCommandDispatcher>();

            builder.Register(x => new SeparatedContextsHandler(x.Resolve<ILifetimeScope>()))
               .As<IAsyncCommandHandler>();

            builder.Register(x => new BackgroundThreadWorker(x.Resolve<IAsyncCommandQueue>(), x.Resolve<IAsyncCommandHandler>()))
                .AsSelf();
        }
    }
}
