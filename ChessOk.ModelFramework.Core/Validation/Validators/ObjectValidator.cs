namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Составной валидатор, производит валидацию объекта путем 
    /// вызова <see cref="AttributesValidator"/> и <see cref="ValidatableObjectValidator"/>.
    /// </summary>
    public class ObjectValidator : CompositeValidator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ObjectValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public ObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            AttributesValidator = ValidationContext.Context.Get<AttributesValidator>();
            ValidatableObjectValidator = ValidationContext.Context.Get<ValidatableObjectValidator>();

            Validators = new IValidator[]
                {
                    AttributesValidator,
                    ValidatableObjectValidator
                };
        }

        /// <summary>
        /// Получает <see cref="AttributesValidator"/>, используемый классом <see cref="ObjectValidator"/>
        /// для валидации свойств проверяемого объекта.
        /// </summary>
        public AttributesValidator AttributesValidator { get; private set; }

        /// <summary>
        /// Получает <see cref="ValidatableObjectValidator"/>, используемый классом <see cref="ObjectValidator"/>
        /// для валидации объектов, реализующих интерфейс <see cref="IValidatable"/>.
        /// </summary>
        public ValidatableObjectValidator ValidatableObjectValidator { get; private set; }
    }
}
