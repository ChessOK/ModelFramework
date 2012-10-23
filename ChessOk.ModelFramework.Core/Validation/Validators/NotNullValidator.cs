namespace ChessOk.ModelFramework.Validation.Validators
{
    public class NotNullValidator : DelegateValidator
    {
        public NotNullValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Delegate = x => x != null;
            Message = Resources.Strings.NotNullValidatorMessage;
        }
    }
}