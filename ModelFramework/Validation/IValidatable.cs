using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Базовый интерфейс для классов, которые реализуют самостоятельную
    /// процедуру валидации. 
    /// </summary>
    /// <remarks>
    /// Вызов <see cref="Validate"/> осуществляется внутри валидатора <see cref="ValidatableObjectValidator"/>,
    /// а также всех валидаторов, которые его используют, например, <see cref="ObjectValidator"/>, 
    /// <see cref="ValidAttribute"/> и <see cref="EnsureSyntaxExtensions.IsValid{TObject}"/>
    /// </remarks>
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
