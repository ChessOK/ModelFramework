using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Compatibility
{
    public class MinLengthAttribute : ValidateAttribute
    {
        private readonly int _minimumLength;

        public MinLengthAttribute(int minimumLength)
        {
            _minimumLength = minimumLength;
        }

        public string ErrorMessage { get; set; }

        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Get<MinLengthValidator>();
            validator.Length = _minimumLength;
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}