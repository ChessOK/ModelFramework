using System.Collections.Generic;
using System.Linq;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию на основе списка валидаторов,
    /// указываемых в свойстве <see cref="Validators"/>.
    /// </summary>
    public class CompositeValidator : Validator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CompositeValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public CompositeValidator(IValidationContext validationContext)
            : base(validationContext)
        {
            Validators = Enumerable.Empty<IValidator>();
        }

        /// <summary>
        /// Получает или задает список валидаторов, вызываемых
        /// функцией <see cref="Validate"/>.
        /// </summary>
        public IEnumerable<IValidator> Validators { get; set; }

        /// <summary>
        /// Проверяет указанный <paramref name="obj"/> на корректность,
        /// используя указанные в свойстве <see cref="Validators"/> валидаторы.
        /// </summary>
        /// <param name="obj">Проверяемый объект.</param>
        public override void Validate(object obj)
        {
            foreach (var validator in Validators)
            {
                validator.Validate(obj);
            }
        }
    }
}
