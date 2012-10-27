namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ObjectValidator : CompositeValidator
    {
        public ObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            AttributesValidator = ValidationContext.Model.Get<AttributesValidator>();
            ValidatableObjectValidator = ValidationContext.Model.Get<ValidatableObjectValidator>();

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
