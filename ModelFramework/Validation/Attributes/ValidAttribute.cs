using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Указывает, что объект нужно проверить с помощью <see cref="ObjectValidator"/>.
    /// </summary>
    public class ValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Инициализирует экземпляр атрибута <see cref="ValidAttribute"/>.
        /// </summary>
        public ValidAttribute()
        {
        }

        /// <summary>
        /// Получить экземпляр типа <see cref="ObjectValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Context.Get<ObjectValidator>();
            return validator;
        }
    }
}
