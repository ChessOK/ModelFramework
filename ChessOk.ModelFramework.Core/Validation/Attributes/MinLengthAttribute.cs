using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Compatibility
{
    /// <summary>
    /// Указывает, что минимальная длина объекта (строки, либо массива),
    /// не должна быть мешьше указанной в конструкторе длины.
    /// </summary>
    /// <remarks>
    /// Для проверки используется валидатор <see cref="MinLengthValidator"/>,
    /// для более детальных сведений см. его документацию.
    /// </remarks>
    public class MinLengthAttribute : ValidateAttribute
    {
        private readonly int _minimumLength;

        /// <summary>
        /// Создать экземпляр атрибута <see cref="MinLengthAttribute"/>
        /// на основе параметра <paramref name="minimumLength"/>.
        /// </summary>
        /// <param name="minimumLength">Минимальная длина строки, либо массива.</param>
        public MinLengthAttribute(int minimumLength)
        {
            _minimumLength = minimumLength;
        }

        /// <summary>
        /// Получает или задает сообщение, показываемое при ошибке. Может быть пустым.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получить экземпляр типа <see cref="MinLengthValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = _minimumLength;
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}