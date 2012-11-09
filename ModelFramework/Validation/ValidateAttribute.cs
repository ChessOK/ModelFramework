using System;

using Autofac;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Служит базовым классом для всех валидационных атрибутов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidateAttribute : Attribute
    {
        /// <summary>
        /// Получает экземпляр класса, реализующего <see cref="IValidator"/>,
        /// ассоциированного с данным атрибутом.
        /// </summary>
        /// <returns></returns>
        public abstract IValidator GetValidator(ILifetimeScope scope);
    }
}
