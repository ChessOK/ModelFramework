namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию объекта, реализующего интерфейс <see cref="IValidatable"/>,
    /// вызывая его метод <see cref="IValidatable.Validate"/>.
    /// </summary>
    public class ValidatableObjectValidator : Validator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidatableObjectValidator"/>,
        /// используя заданный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст</param>
        public ValidatableObjectValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        /// <summary>
        /// Вызывает метод <see cref="IValidatable.Validate"/>, если
        /// <paramref name="obj"/> реализует интерфейс <see cref="IValidatable"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если значени указанного объекта равено <c>null</c>, либо не реализует 
        /// нужный интерфейс, валидация всегда проходит успешно. 
        /// Используйте <see cref="RequiredValidator"/>,  если хотите проверить, 
        /// имеет ли объект значение.
        /// </remarks>
        /// 
        /// <param name="obj">Проверяемый объект.</param>
        public override void Validate(object obj)
        {
            var validatable = obj as IValidatable;
            if (validatable != null)
            {
                validatable.Validate(ValidationContext);
            }
        }
    }
}
