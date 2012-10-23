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

        public override void Validate(object value)
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
