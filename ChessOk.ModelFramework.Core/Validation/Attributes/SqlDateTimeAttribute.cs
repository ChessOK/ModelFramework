using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Указывает, что объект должен быть датой, сериализуемой в формат datetime в MS SQLServer.
    /// </summary>
    /// <remarks>
    /// Для проверки используется валидатор <see cref="SqlDateTimeValidator"/>,
    /// для более детальных сведений см. его документацию.
    /// </remarks>
    public class SqlDateTimeAttribute : ValidateAttribute
    {
        /// <summary>
        /// Получает или задает сообщение, показываемое при ошибке. Может быть пустым.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получить экземпляр типа <see cref="SqlDateTimeValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Model.Get<SqlDateTimeValidator>();
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
