using Autofac;

using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationBus(x.Resolve<IContext>()))
                .As<IApplicationBus>();

            builder.Register(x => new CommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<IApplicationEventHandler>().InstancePerApplicationBus();
            builder.Register(x => new ValidationContext(x.Resolve<IContext>()))
                .As<IValidationContext>().InstancePerApplicationBus();

            builder.RegisterGeneric(typeof(SaveCommand<>)).AsSelf();
            builder.RegisterGeneric(typeof(DeleteCommand<>)).AsSelf();
        }
    }
}
