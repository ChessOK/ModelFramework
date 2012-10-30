using Autofac;

using ChessOk.ModelFramework.Validation.Internals;
using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Модуль, регистрирующий <see cref="IValidationContext"/>
    /// и все стандартные валидаторы.
    /// </summary>
    public class ValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new ValidationContext(x.Resolve<IModelContext>()))
                .As<IValidationContext>().InstancePerApplicationBus();

            builder.Register(x => new AttributesValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new CollectionValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new CompositeValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new DelegateValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new NullValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new ObjectValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new RequiredValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new SqlDateTimeValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new MaxLengthValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new ValidatableObjectValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new RegularExpressionValidator(x.Resolve<IValidationContext>())).AsSelf();
            builder.Register(x => new MinLengthValidator(x.Resolve<IValidationContext>())).AsSelf();
        }
    }
}
