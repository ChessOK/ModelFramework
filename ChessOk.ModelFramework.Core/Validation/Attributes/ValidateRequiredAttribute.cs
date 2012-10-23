using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateRequiredAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Get<RequiredValidator>();
        }
    }
}
