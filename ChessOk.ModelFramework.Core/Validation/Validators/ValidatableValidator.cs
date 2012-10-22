namespace ChessOk.ModelFramework.Validation.Validators
{
    public class ValidatableObjectValidator : IValidator
    {
        public IValidationContext ValidationContext { get; set; }

        public void Validate(object obj)
        {
            var validatable = obj as IValidatable;
            if (validatable != null)
            {
                validatable.Validate(ValidationContext);
            }
        }
    }
}
