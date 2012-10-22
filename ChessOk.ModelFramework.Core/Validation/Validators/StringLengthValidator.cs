using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class MaximumLengthValidator : IValidator
    {
        private readonly int _maximumLength;

        public MaximumLengthValidator(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        public IValidationContext ValidationContext { get; set; }

        public void Validate(object value)
        {
            var str = (string)value;
            if (str == null) { return; }

            if (str.Length > _maximumLength)
            {
                ValidationContext.AddError(String.Format(Resources.Strings.MaximumLengthValidatorMessage, _maximumLength));
            }
        }
    }
}
