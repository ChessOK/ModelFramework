using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию, сравнивая длину строки или
    /// массива с заданной максимальной длиной, указанной в свойстве <see cref="Length"/>.
    /// </summary>
    public class MaxLengthValidator : IValidator
    {
        /// <summary>
        /// Получает или задает максимальную длину строки, либо массива. 
        /// Если значение равна "-1", то валидация всегда завершается успешно (введено 
        /// для совместимости с MaxLengthAttribute из DataAnnotations).
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// <para>Получает или задает сообщение, добавляемое в валидационный контекст
        /// при неудачной валидации.</para>
        /// <para>Если сообщение не задано, то используется сообщение по-умолчанию.</para>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Проверяет длину строки, либо массива, указанного в параметре <paramref name="value"/>
        /// сравнивая её с максимальной длиной, указанной в свойстве <see cref="Length"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если длина объекта больше, чем указанное число, то в валидационный
        /// контекст добавляется сообщение об ошибке, указанное в свойстве <see cref="Message"/>.
        /// 
        /// Если значение проверяемого объекта равно <c>null</c>, то валидация всегда 
        /// проходит успешно. Используйте <see cref="RequiredValidator"/>,
        /// если хотите проверить, имеет ли объект значение.
        /// 
        /// В качестве ключей для ошибок выступает пустая строка.
        /// </remarks>
        /// 
        /// <param name="value">Проверяемый объект.</param>
        /// <param name="context">Валидационный контекст.</param>
        /// 
        /// <exception cref="InvalidOperationException"><see cref="Length"/> имеет отрицательное значение.</exception>
        /// <exception cref="InvalidOperationException">
        ///   <paramref name="value"/> задан, но не является строкой или массивом.
        /// </exception>
        public void Validate(IValidationContext context, object value)
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
                context.AddError(String.Format(Message ?? Resources.Strings.MaxLengthValidatorMessage, Length));
            }
        }
    }
}
