using System.ComponentModel.DataAnnotations;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// <para>Указывает, что строковое представления объекта не должно быть
    /// равным строковому представлению объекта, указанного в конструкторе.</para>
    /// <para>Вместо <c>NotEquals(null)</c> используйте <see cref="RequiredAttribute"/></para>
    /// </summary>
    public class NotEqualsAttribute : ValidateAttribute
    {
        private readonly object _obj;

        /// <summary>
        /// Инициализирует экземпляр атрибута <see cref="NotEqualsAttribute"/>,
        /// используя параметр <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">Объект, строковое представление которого не должно быть равно текущему объекту.</param>
        public NotEqualsAttribute(object obj)
        {
            _obj = obj;
        }

        /// <summary>
        /// Получает или задает сообщение, показываемое при ошибке. Может быть пустым.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Получить экземпляр типа <see cref="DelegateValidator"/>.
        /// </summary>
        /// <returns>Экземпляр валидатора.</returns>
        public override IValidator GetValidator()
        {
            var validator = ValidationContext.Context.Get<DelegateValidator>();
            validator.Delegate = x =>
                {
                    var left = x != null ? x.ToString() : null;
                    var right = _obj != null ? _obj.ToString() : null;

                    return left != right;
                };
            validator.Message = ErrorMessage;

            return validator;
        }
    }
}
