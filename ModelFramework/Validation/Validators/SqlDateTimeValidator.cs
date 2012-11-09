using System;
using System.Data.SqlTypes;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Проверяет дату на корректность в рамках значения поля с типом datetime в Microsoft SQLServer.
    /// </summary>
    /// 
    /// <remarks>
    /// Год в дате должен быть больше 1753 и меньше 9999.
    /// 
    /// Если значение проверяемого объекта равно <c>null</c>, то валидация 
    /// завершается успешно. Используйте <see cref="RequiredValidator"/>,
    /// если хотите проверить, имеет ли объект значение.
    /// 
    /// В качестве ключа ошибки выступает пустая строка.
    /// </remarks>
    public class SqlDateTimeValidator : IValidator
    {
        /// <summary>
        /// <para>Получает или задает сообщение, добавляемое в валидационный
        /// контекст при неудачной валидации.</para>
        /// <para>Если значение отсутствует, то используется сообщение по-умолчанию.</para>
        /// </summary>
        public string Message { get; set; }

        
        /// <summary>
        /// Проверяет указанную в <paramref name="value"/> дату на корректность.
        /// </summary>
        /// 
        /// <param name="value">Проверяемая дата</param>
        /// <param name="context">Валидационный контекст.</param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///   Параметр <paramref name="value"/> задан, но не является экземпляром типа <see cref="DateTime"/>.
        /// </exception>
        public void Validate(IValidationContext context, object value)
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
                context.AddError(Message ?? Resources.Strings.SqlDateTimeValidatorMessage);
            }
        }
    }
}
