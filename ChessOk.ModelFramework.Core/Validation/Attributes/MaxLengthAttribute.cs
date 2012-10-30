using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Compatibility
{
    /// <summary>
    /// Указывает, что максимальная длина объекта (строки, либо массива),
    /// не должна превышать указанную в конструкторе длину.
    /// </summary>
    /// <remarks>
    /// Для проверки используется валидатор <see cref="MaxLengthValidator"/>,
    /// для более детальных сведений см. его документацию.
    /// </remarks>
    public class MaxLengthAttribute : ValidateAttribute
    {
        private readonly int _maximumLength;

        /// <summary>
        /// Инициализирует экземпляр атрибута <see cref="MaxLengthAttribute"/>
        /// на основе параметра <paramref name="maximumLength"/>.
        /// </summary>
        /// <param name="maximumLength">Максимальная длина строки, либо массива.</param>
        public MaxLengthAttribute(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        /// <summary>
        /// Получает или задает сообщение, показываемое при ошибке. Может быть пустым.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получить экземпляр типа <see cref="MaxLengthValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Context.Get<MaxLengthValidator>();
            validator.Length = _maximumLength;
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
