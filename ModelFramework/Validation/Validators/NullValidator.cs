namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию объектов, причем проверка завершается успешно, 
    /// если проверяемый объект равен значению <c>null</c>.
    /// </summary>
    public class NullValidator : DelegateValidator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NullValidator"/>.
        /// </summary>
        public NullValidator()
        {
            Delegate = x => x == null;
            Message = Resources.Strings.NullValidatorMessage;
        }
    }
}
