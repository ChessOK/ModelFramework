using System;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Служит базовым классом для всех валидационных атрибутов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidateAttribute : Attribute
    {
        /// <summary>
        /// Получает экземпляр класса, реализующего <see cref="IValidationContext"/>,
        /// ассоциированного с данным экземпляром атрибута.
        /// </summary>
        public IValidationContext ValidationContext { get; internal set; }

        /// <summary>
        /// Получает экземпляр класса, реализующего <see cref="IValidator"/>,
        /// ассоциированного с данным атрибутом.
        /// </summary>
        /// <returns></returns>
        public abstract IValidator GetValidator();
    }
}
