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
            var attributesValidator = ValidationContext.Context.Get<AttributesValidator>();
            var validatableObjectValidator = ValidationContext.Context.Get<ValidatableObjectValidator>();

            Validators = new IValidator[]
                {
                    attributesValidator,
                    validatableObjectValidator
                };
        }
    }
}
