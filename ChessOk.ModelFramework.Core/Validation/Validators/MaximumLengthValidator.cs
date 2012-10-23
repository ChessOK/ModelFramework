using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class MaximumLengthValidator : Validator
    {
        public MaximumLengthValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public int MaximumLength { get; set; }

        public override void Validate(object value)
        {
            var str = (string)value;
            if (str == null) { return; }

            if (str.Length > MaximumLength)
            {
                ValidationContext.AddError(String.Format(Resources.Strings.MaximumLengthValidatorMessage, MaximumLength));
            }
        }
    }
}
