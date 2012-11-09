using Autofac;

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

            builder.Register(x => new AttributesValidator()).AsSelf().SingleInstance();
            builder.Register(x => new CollectionValidator(x.Resolve<ILifetimeScope>())).AsSelf().SingleInstance();
            builder.Register(x => new DelegateValidator()).AsSelf();
            builder.Register(x => new NullValidator()).AsSelf();
            builder.Register(x => new ObjectValidator(x.Resolve<ILifetimeScope>())).AsSelf().SingleInstance();
            builder.Register(x => new RequiredValidator()).AsSelf();
            builder.Register(x => new SqlDateTimeValidator()).AsSelf();
            builder.Register(x => new MaxLengthValidator()).AsSelf();
            builder.Register(x => new ValidatableObjectValidator()).AsSelf().SingleInstance();
            builder.Register(x => new RegularExpressionValidator()).AsSelf();
            builder.Register(x => new MinLengthValidator()).AsSelf();
        }
    }
}
