namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию объектов, причем проверка завершается успешно, 
    /// если проверяемый объект равен значению <c>null</c>.
    /// </summary>
    public class NullValidator : DelegateValidator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NullValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext"></param>
        public NullValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Delegate = x => x == null;
            Message = Resources.Strings.NullValidatorMessage;
        }
    }
}
