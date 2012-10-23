using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateObjectAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Get<ObjectValidator>();
        }
    }
}
