using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию на основе полного совпадения строкового представления
    /// проверяемого объекта с регулярным выражением, указанным в 
    /// свойстве <see cref="Pattern"/>.
    /// </summary>
    public class RegularExpressionValidator : Validator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="RegularExpressionValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public RegularExpressionValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        /// <summary>
        /// Получает или задает регулярное выражение, с которым сравнивается строковое
        /// представление проверяемого объекта.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// <para>Получает или задает сообщение, добавляемое в валидационный контекст, если
        /// валидация завершилась неуспешно.</para>
        /// <para>Если не указано, то используется сообщение по-умолчанию.</para>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Проверяет строковое представление указанного объекта на
        /// соответствие с указанным в свойстве <see cref="Pattern"/> 
        /// регулярным выражением, которое должно соответствовать всей строке.
        /// </summary>
        /// 
        /// <remarks>
        /// Если строковое представление удовлетворяет <c>String.IsNullOrEmpty</c>,
        /// то валидация всегда проходит успешно. Используйте <see cref="RequiredValidator"/>,
        /// если хотите проверить, имеет ли объект значение.
        /// 
        /// В качестве ключей для ошибок выступает пустая строка.</remarks>
        /// 
        /// <param name="obj">Проверяемый объект.</param>
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
