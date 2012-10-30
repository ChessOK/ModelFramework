using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Internals
{
    /// <summary>
    /// Указывает, что объект нужно проверить с помощью
    /// <see cref="AttributesValidator"/>.
    /// </summary>
    public class ValidateAttributesAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получить экземпляр типа <see cref="AttributesValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<AttributesValidator>();
        }
    }
}
