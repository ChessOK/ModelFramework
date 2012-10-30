using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Базовый класс для валидаторов, имеющих <see cref="ValidationContext"/>.
    /// </summary>
    public abstract class Validator : IValidator
    {
        /// <summary>
        /// Инициализирует экземпляр объекта, используя
        /// указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        /// <exception cref="ArgumentNullException">
        ///   Параметр <paramref name="validationContext"/> не указан.
        /// </exception>
        protected Validator(IValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            ValidationContext = validationContext;
        }

        /// <summary>
        /// Получает или задает валидационный контекст, привязанный к валидатору.
        /// </summary>
        public IValidationContext ValidationContext { get; private set; }

        /// <summary>
        /// Абстрактный метод, который производит валидацию указанного объекта 
        /// <paramref name="obj"/>, используя механизмы, определенные в классе-наследнике.
        /// </summary>
        /// <param name="obj">Проверяемый объект.</param>
        public abstract void Validate(object obj);
    }
}
