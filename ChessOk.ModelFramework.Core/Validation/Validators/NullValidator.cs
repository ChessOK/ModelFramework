namespace ChessOk.ModelFramework.Validation.Validators
{
    public class NullValidator : DelegateValidator
    {
        public NullValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Delegate = x => x == null;
            Message = Resources.Strings.NullValidatorMessage;
        }
    }
}
