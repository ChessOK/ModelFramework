using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Указывает, что объект должен иметь значение <c>null</c>.
    /// </summary>
    /// <remarks>
    /// Для проверки используется валидатор <see cref="NullValidator"/>,
    /// для более детальных сведений см. его документацию.
    /// </remarks>
    public class NullAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получает или задает сообщение, показываемое при ошибке. Может быть пустым.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получить экземпляр типа <see cref="NullValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Context.Get<NullValidator>();
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
