using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет базовый интерфейс для валидаторов.
    /// </summary>
    /// 
    /// <remarks>
    /// Реализация данного интерфейса должна быть потокобезопасной.
    /// </remarks>
    public interface IValidator
    {
        /// <summary>
        /// Производит валидацию указанного объекта <paramref name="obj"/>, 
        /// в соответствии с правелами, определенными в реализации интерфейса.
        /// </summary>
        /// <param name="context">Валидационный контекст.</param>
        /// <param name="obj">Проверяемый объект.</param>
        void Validate(IValidationContext context, object obj);
    }
}
