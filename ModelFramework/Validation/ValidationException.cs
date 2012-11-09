using System;
using System.Linq;
using System.Text;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Исключение, генерируемое <see cref="IValidationContext.ThrowExceptionIfInvalid"/> и
    /// поясняющее, что поток выполнения был прерван из-за наличия хотя бы одной валидационной ошибки.
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ValidationException"/>,
        /// используя <paramref name="context"/>.
        /// </summary>
        /// <param name="context">Валидационный контекст.</param>
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
