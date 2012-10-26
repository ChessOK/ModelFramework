using Autofac;

using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationBus(x.Resolve<IContext>()))
                .As<IApplicationBus>();
            
            builder.RegisterModule(new CommandsModule());
            builder.RegisterModule(new AsyncCommandsModule());
            builder.RegisterModule(new ValidationModule());
        }
    }
}
