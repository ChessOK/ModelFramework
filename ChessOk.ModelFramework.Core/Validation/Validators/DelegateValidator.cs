using System;

namespace ChessOk.ModelFramework.Validation.Validators
{
    /// <summary>
    /// Производит валидацию на основе заданного в свойстве <see cref="Delegate"/>
    /// делегата.
    /// </summary>
    public class DelegateValidator : Validator
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="DelegateValidator"/>,
        /// используя указанный <paramref name="validationContext"/>.
        /// </summary>
        /// <param name="validationContext"></param>
        public DelegateValidator(IValidationContext validationContext)
            : base(validationContext)
        {
        }

        /// <summary>
        /// Получает или задает делегат, на основе которого выполняется проверка.
        /// </summary>
        /// 
        /// <remarks>
        /// При проверке делегат получает на вход проверяемый объект.
        /// Он должен вернуть <c>true</c>, если объект корректен и <c>false</c>
        /// в противном случае.
        /// </remarks>
        public Func<object, bool> Delegate { get; set; }

        /// <summary>
        /// Получает или задает сообщение, добавляемое в валидационный контекст при
        /// неудачной валидации.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Проверяет заданный <paramref name="obj"/> на корректность,
        /// используя делегат, заданный в свойстве <see cref="Delegate"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если валидация завершилась неудачей, то в валидационный
        /// контекст добавляется ошибка с сообщением, указанным в свойстве 
        /// <see cref="Message"/>.
        /// 
        /// В качестве ключей для ошибок выступает пустая строка.
        /// </remarks>
        /// 
        /// <param name="obj">Проверяемый объект.</param>
        /// <exception cref="InvalidOperationException">Свойство <see cref="Delegate"/> не задано.</exception>
        public override void Validate(object obj)
        {
            if (Delegate == null)
            {
                throw new InvalidOperationException("Delegate property is not set.");
            }

            if (!Delegate(obj))
            {
                ValidationContext.AddError(Message);
            }
        }
    }
}
