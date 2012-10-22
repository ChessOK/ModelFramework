using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateNullAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return new NullValidator();
        }
    }
}
