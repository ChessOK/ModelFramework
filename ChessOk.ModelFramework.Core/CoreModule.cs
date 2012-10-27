using Autofac;

using ChessOk.ModelFramework.AsyncCommands;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Scopes;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationBus(x.Resolve<IModelScope>()))
                .As<IApplicationBus>();
            
            builder.RegisterModule(new CommandsModule());
            builder.RegisterModule(new AsyncCommandsModule());
            builder.RegisterModule(new ValidationModule());
        }
    }
}
