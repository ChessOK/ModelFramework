using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет базовый интерфейс для валидаторов.
    /// <seealso cref="Validator"/>
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Получает экземпляр <see cref="IValidationContext"/>,
        /// ассоциированный с данным экземпляром валидатора.
        /// </summary>
        IValidationContext ValidationContext { get; }

        /// <summary>
        /// Производит валидацию указанного объекта <paramref name="obj"/>, 
        /// в соответствии с правелами, определенными в реализации интерфейса.
        /// </summary>
        /// <param name="obj">Проверяемый объект.</param>
        void Validate(object obj);
    }
}
