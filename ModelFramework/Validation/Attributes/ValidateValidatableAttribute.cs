using Autofac;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Internals
{
    /// <summary>
    /// Указывает, что объект нужно проверить, используя <see cref="ValidatableObjectValidator"/>.
    /// </summary>
    public class ValidateValidatableAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получить экземпляр типа <see cref="ValidatableObjectValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator(ILifetimeScope scope)
        {
            return scope.Resolve<ValidatableObjectValidator>();
        }
    }
}
