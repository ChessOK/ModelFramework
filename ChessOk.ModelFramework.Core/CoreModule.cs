using Autofac;

using ChessOk.ModelFramework.AsyncCommands.Internals;
using ChessOk.ModelFramework.Commands;
using ChessOk.ModelFramework.Commands.Internals;
using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Internals;
using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationBus(x.Resolve<IContext>()))
                .As<IApplicationBus>();
            builder.Register(x => new ValidationContext(x.Resolve<IContext>()))
                .As<IValidationContext>().InstancePerApplicationBus();

            builder.Register(x => new CommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<IApplicationBusMessageHandler>().InstancePerApplicationBus();
            builder.Register(x => new AsyncCommandDispatcher(x.Resolve<IApplicationBus>()))
                .As<IApplicationBusMessageHandler>().InstancePerApplicationBus();

            builder.Register(x => new AttributesValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new CollectionValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new CompositeValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new DelegateValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new NotNullValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new NullValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new ObjectValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new RequiredValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new SqlDateTimeValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new MaximumLengthValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new ValidatableObjectValidator(x.Resolve<IValidationContext>())).AsSelf();
        }
    }
}
