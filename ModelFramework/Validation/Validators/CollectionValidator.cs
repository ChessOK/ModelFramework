using System;
using System.Collections.Generic;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию элементов коллекции, последовательно применяя
    /// к ним валидатор <see cref="ObjectValidator"/>.
    /// </summary>
    public class CollectionValidator : Validator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CollectionValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext">Валидационный контекст.</param>
        public CollectionValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        /// <summary>
        /// Проверяет указанную в параметре <paramref name="obj"/> коллекцию,
        /// последовательно применяя к каждому её элементу валидатор <see cref="ObjectValidator"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// При валидации производится замена ключей ошибок, добавляя к ним префикс "[&lt;индекс&gt;].",
        /// аналогичный префиксу, добавляемого ASP.NET MVC для элементов форм при рендеринге списка вложенных объектов.
        /// 
        /// Если проверяемый объект равен <c>null</c>, то валидация завершается успешно. Используйте <see cref="RequiredValidator"/>,
        /// если хотите проверить, имеет ли объект значение.
        /// </remarks>
        /// 
        /// <param name="obj">Проверяемая коллекция.</param>
        /// <exception cref="InvalidOperationException">Указанный <paramref name="obj"/> задан, но не является коллекцией.</exception>
        public override void Validate(object obj)
        {
            if (obj == null) { return; }

            var enumerable = obj as IEnumerable<object>;

            if (enumerable == null)
            {
                throw new InvalidOperationException(
                    String.Format(Resources.Strings.CollectionValidatorInvalidObject, GetType(), obj.GetType()));
            }

            var index = 0;
            foreach (var o in enumerable)
            {
                // Если исходный ключ был пустым, то просто добавляем индекс.
                using (ValidationContext.ModifyKeys("^$", string.Format("[{0}]", index)))
                // Если исходным ключ не был пустым, то добавляем индекс с точкой.
                using (ValidationContext.ModifyKeys("^(.+)$", string.Format("[{0}].$1", index)))
                {
                    var validator = new ObjectValidator(ValidationContext);
                    validator.Validate(o);
                }

                index++;
            }
        }
    }
}
