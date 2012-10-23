using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidatePresenceAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Get<PresenceValidator>();
        }
    }
}
