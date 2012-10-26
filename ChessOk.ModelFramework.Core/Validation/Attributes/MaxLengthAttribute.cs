using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation.Compatibility
{
    public class MaxLengthAttribute : ValidateAttribute
    {
        private readonly int _maximumLength;

        public MaxLengthAttribute(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        public string ErrorMessage { get; set; }

        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Get<MaxLengthValidator>();
            validator.Length = _maximumLength;
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
