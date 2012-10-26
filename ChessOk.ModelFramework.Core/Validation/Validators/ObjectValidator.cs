namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ObjectValidator : CompositeValidator
    {
        public ObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            AttributesValidator = ValidationContext.Get<AttributesValidator>();
            ValidatableObjectValidator = ValidationContext.Get<ValidatableObjectValidator>();

            Validators = new IValidator[]
                {
                    AttributesValidator,
                    ValidatableObjectValidator
                };
        }

        public AttributesValidator AttributesValidator { get; private set; }
        public ValidatableObjectValidator ValidatableObjectValidator { get; private set; }
    }
}
