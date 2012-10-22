using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidateMaximumLengthAttribute : ValidateAttribute
    {
        private readonly int _maximumLength;

        public ValidateMaximumLengthAttribute(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        public override IValidator GetValidator()
        {
            return new MaximumLengthValidator(_maximumLength);
        }
    }
}
