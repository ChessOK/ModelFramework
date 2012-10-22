namespace ChessOk.ModelFramework.Validation.Validators
{
    public class NotNullValidator : DelegateValidator<object>
    {
        public NotNullValidator()
            : base(x => x != null, Resources.Strings.NotNullValidatorMessage)
        {
        }
    }
}