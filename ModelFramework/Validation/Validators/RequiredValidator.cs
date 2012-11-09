namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию строки или объекта, причем валидация
    /// завершается успешно, если значение объекта не равно <c>null</c>,
    /// и строка не является пустой (если свойство <see cref="AllowEmptyStrings"/>
    /// имеет значение <c>false</c>).
    /// </summary>
    public class RequiredValidator : IValidator
    {
        /// <summary>
        /// Получает или задает значение, разрешать ли
        /// использование пустых строк в качестве проверяемого 
        /// объекта (по-умолчанию <c>false</c>).
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        /// <summary>
        /// Получает или задает сообщение, добавляемое в валидационный
        /// контекст, если валидация завершилась неудачно.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Проверяет указанную в параметрах <paramref name="obj"/> строку
        /// или объект. Валидация завершается успешно, если значение объекта не равно <c>null</c>
        /// и не равно пустой строке (если объектом является строка и свойство <see cref="AllowEmptyStrings"/>
        /// имеет значение <c>false</c>).
        /// </summary>
        /// 
        /// <remarks>
        /// В качестве включей ошибок выступает пустая строка.
        /// </remarks>
        /// 
        /// <param name="obj">Проверяемый объект.</param>
        /// <param name="context">Валидационный контекст.</param>
        public void Validate(IValidationContext context, object obj)
        {
            var message = Message ?? Resources.Strings.PresenceValidatorMessage;

            if (obj == null)
            {
                context.AddError(message);
            }
            
            var str = obj as string;
            if (str != null)
            {
                if (!AllowEmptyStrings && str.Trim().Length == 0)
                {
                    context.AddError(message);
                }
            }
        }
    }
}
