using System;
using System.Linq;
using System.Text;

namespace ChessOk.ModelFramework.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(IValidationContext context)
            : base(FormatMessage(context))
        {
        }

        private static string FormatMessage(IValidationContext context)
        {
            var sb = new StringBuilder();
            sb.AppendLine("One or more validation errors occured:");
            foreach (var key in context.Keys)
            {
                sb.AppendLine(String.Format(
                    "Key {0}: {1}", 
                    !String.IsNullOrEmpty(key) ? "\"" + key + "\"" : "<empty>", 
                    String.Join(", ", context[key].Select(x => "\"" + x + "\""))));
            }

            return sb.ToString();
        }
    }
}
