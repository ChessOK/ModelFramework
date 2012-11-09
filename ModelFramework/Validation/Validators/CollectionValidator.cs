using System;
using System.Collections.Generic;

using Autofac;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию элементов коллекции, последовательно применяя
    /// к ним валидатор <see cref="ObjectValidator"/>.
    /// </summary>
    public class CollectionValidator : IValidator
    {
        private readonly ObjectValidator _objectValidator;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CollectionValidator"/>,
        /// используя <paramref name="lifetimeScope"/>.
        /// </summary>
        public CollectionValidator(ILifetimeScope lifetimeScope)
        {
            _objectValidator = lifetimeScope.Resolve<ObjectValidator>();
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
        /// <param name="context">Валидационный контекст.</param>
        /// 
        /// <exception cref="InvalidOperationException">Указанный <paramref name="obj"/> задан, но не является коллекцией.</exception>
        public void Validate(IValidationContext context, object obj)
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
                using (context.ModifyKeys("^$", string.Format("[{0}]", index)))
                // Если исходным ключ не был пустым, то добавляем индекс с точкой.
                using (context.ModifyKeys("^(.+)$", string.Format("[{0}].$1", index)))
                {
                    _objectValidator.Validate(context, o);
                }

                index++;
            }
        }
    }
}
