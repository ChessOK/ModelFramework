namespace ChessOk.ModelFramework.Validation.Validators
{
    public class NullValidator : DelegateValidator<object>
    {
        public NullValidator()
            : base(x => x == null, Resources.Strings.NullValidatorMessage)
        {
        }
    }
}
