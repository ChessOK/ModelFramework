using Autofac;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Указывает, что объект нужно проверить с помощью <see cref="ObjectValidator"/>.
    /// </summary>
    public class ValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получить экземпляр типа <see cref="ObjectValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator(ILifetimeScope scope)
        {
            var validator = scope.Resolve<ObjectValidator>();
            return validator;
        }
    }
}
