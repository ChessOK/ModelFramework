using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ChessOk.ModelFramework.Validation.Validators
{
    public class RegularExpressionValidator : Validator
    {
        public RegularExpressionValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        public string Pattern { get; set; }

        public string Message { get; set; }

        public override void Validate(object obj)
        {
            var input = Convert.ToString(obj, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(input)) { return; }

            var match = Regex.Match(input, Pattern);

            if (!match.Success || match.Index != 0 || match.Length != input.Length)
            {
                ValidationContext.AddError(String.Format(Message ?? Resources.Strings.RegularExpressionMessage, Pattern));
            }
        }
    }
}
