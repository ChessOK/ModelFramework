using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Internals
{
    public class ValidateValidatableAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            return ValidationContext.Model.Get<ValidatableObjectValidator>();
        }
    }
}
