using System;
using System.Data.SqlTypes;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class SqlDateTimeValidator : IValidator
    {
        public IValidationContext ValidationContext { get; set; }

        public void Validate(object value)
        {
            var dateTime = (DateTime)value;

            var isValid = ((dateTime >= (DateTime)SqlDateTime.MinValue) && (dateTime <= (DateTime)SqlDateTime.MaxValue));
            if (!isValid)
            {
                ValidationContext.AddError(Resources.Strings.SqlDateTimeValidatorMessage);
            }
        }
    }
}
