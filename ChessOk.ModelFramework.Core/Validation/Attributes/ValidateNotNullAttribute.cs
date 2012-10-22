using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateNotNullAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return new NotNullValidator();
        }
    }
}
