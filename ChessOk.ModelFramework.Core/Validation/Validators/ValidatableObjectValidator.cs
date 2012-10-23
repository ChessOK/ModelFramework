namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ValidatableObjectValidator : Validator
    {
        public ValidatableObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public override void Validate(object obj)
        {
            var validatable = obj as IValidatable;
            if (validatable != null)
            {
                validatable.Validate(ValidationContext);
            }
        }
    }
}
