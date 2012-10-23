namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ObjectValidator : CompositeValidator
    {
        public ObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Validators = new IValidator[]
                {
                    ValidationContext.Get<AttributesValidator>(),
                    ValidationContext.Get<ValidatableObjectValidator>()
                };
        }
    }
}
