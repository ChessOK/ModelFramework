using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию, сравнивая длину строки, либо массива, с
    /// заданной минимальной длинной, указанной в свойстве <see cref="Length"/>.
    /// </summary>
    public class MinLengthValidator : Validator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="MinLengthValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public MinLengthValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        /// <summary>
        /// Получает или задает минимальную длину строки, либо массива.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// <para>Получает или задает сообщение, добавляемое в валидационный контекст, 
        /// в случае, если валидация завершилась неуспешно.</para>
        /// <para>Если сообщение не задано, то используется сообщение по-умолчанию.</para>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Проверяет длину строки, либо массива, указанного в параметре <paramref name="value"/>
        /// сравнивая её с минимальной длиной, указанной в свойстве <see cref="Length"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если длина объекта миньше, чем указанное число, то в валидационный
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
        /// <exception cref="InvalidOperationException"><see cref="Length"/> имеет отрицательное значение.</exception>
        /// <exception cref="InvalidOperationException">
        ///   <paramref name="value"/> задан, но не является строкой или массивом.
        /// </exception>
        public override void Validate(object value)
        {
            if (Length < 0)
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

            if (num < Length)
            {
                ValidationContext.AddError(String.Format(Message ?? Resources.Strings.MinLengthValidatorMessage, Length));
            }
        }
    }
}
