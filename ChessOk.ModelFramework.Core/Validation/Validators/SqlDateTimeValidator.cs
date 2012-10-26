using System;
using System.Data.SqlTypes;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class SqlDateTimeValidator : Validator
    {
        public SqlDateTimeValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public string Message { get; set; }

        public override void Validate(object value)
        {
            if (value == null)
            {
                return;
            }

            if (!(value is DateTime))
            {
                throw new InvalidOperationException(
                    String.Format(Resources.Strings.SqlDateTimeValidatorInvalidObject, GetType(), value.GetType()));
            }

            var dateTime = (DateTime)value;

            var isValid = ((dateTime >= (DateTime)SqlDateTime.MinValue) && (dateTime <= (DateTime)SqlDateTime.MaxValue));
            if (!isValid)
            {
                ValidationContext.AddError(Message ?? Resources.Strings.SqlDateTimeValidatorMessage);
            }
        }
    }
}
