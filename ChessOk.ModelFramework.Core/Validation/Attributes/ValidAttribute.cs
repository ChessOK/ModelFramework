using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidAttribute : ValidateAttribute
    {
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Get<ObjectValidator>();
            validator.AttributesValidator.DoNotModifyErrorKeys = DoNotModifyKeys;

            return validator;
        }

        public bool DoNotModifyKeys { get; set; }
    }
}
