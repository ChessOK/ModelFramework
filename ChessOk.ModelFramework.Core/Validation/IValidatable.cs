using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Базовый интерфейс для классов, которые реализуют самостоятельную
    /// процедуру валидации. Проверка осуществляется с помощью <see cref="ValidatableObjectValidator"/>
    /// и использующих его <see cref="ObjectValidator"/>, <see cref="ValidAttribute"/> и
    /// <see cref="EnsureSyntaxExtensions.IsValid{TObject}"/>
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Производит валидацию объекта на основе
        /// операций, реализованных в конечном классе.
        /// </summary>
        /// <param name="context">Валидационный контекст.</param>
        void Validate(IValidationContext context);
    }
}
