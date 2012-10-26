using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class MaxLengthValidator : Validator
    {
        public MaxLengthValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public int Length { get; set; }
        public string Message { get; set; }

        public override void Validate(object value)
        {
            if (Length < -1)
            {
                throw new InvalidOperationException(
                    String.Format(Resources.Strings.LengthValidatorNegativeLength, GetType().Name, Length));
            }

            if (value == null) { return; }

            var str = value as string;
            var arr = value as Array;
            int num = str != null ? str.Length : arr != null ? arr.Length : -1;

            if (num == -1)
            {
                throw new InvalidOperationException(
                    String.Format(Resources.Strings.LengthValidatorInvalidObject, GetType().Name, value.GetType().Name));
            }

            if (Length != -1  && num > Length)
            {
                ValidationContext.AddError(String.Format(Message ?? Resources.Strings.MaxLengthValidatorMessage, Length));
            }
        }
    }
}
