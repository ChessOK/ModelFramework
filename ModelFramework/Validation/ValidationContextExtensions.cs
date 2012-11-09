using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework
{
    /// <summary>
    /// Предоставляет основные расширения для интерфейса <see cref="IValidationContext"/>.
    /// </summary>
    public static class ValidationContextExtensions
    {
        /// <summary>
        /// Начать проверку объекта <paramref name="obj"/>, используя fluent-синтаксис
        /// <see cref="IEnsureSyntax{T}"/> и валидаторы.
        /// </summary>
        /// <typeparam name="T">Тип проверяемого объекта.</typeparam>
        /// <param name="context">Валидационный контекст.</param>
        /// <param name="obj">Проверяемый объект.</param>
        /// <returns>Валидационный синтаксис.</returns>
        public static IEnsureSyntax<T> Ensure<T>(this IValidationContext context, T obj)
        {
            return new EnsureEngine<T>(context, obj);
        }

        /// <summary>
        /// Начать проверку свойства <paramref name="propertyExpression"/> 
        /// объекта <paramref name="obj"/>, используя fluent-синтаксис <see cref="IEnsureSyntax{T}"/> и валидаторы.
        /// Валидация осуществляется в делегате <paramref name="validation"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <typeparam name="V">Тип проверяемого свойства.</typeparam>
        /// <param name="validationContext">Валидационный контекст.</param>
        /// <param name="obj">Объект, свойство которого проверяется.</param>
        /// <param name="propertyExpression">Проверяемое свойство.</param>
        /// <param name="validation">Инструкции по валидации.</param>
        /// <returns>Валидационный синтаксис.</returns>
        public static void Ensure<T, V>(this IValidationContext validationContext,
            T obj, Expression<Func<T, V>> propertyExpression, Action<IEnsureSyntax<V>> validation)
        {
            new EnsureEngine<T>(validationContext, obj).ItsProperty(propertyExpression, validation);
        }

        /// <summary>
        /// Регистрирует ошибку в валидационном контексте, используя в качестве
        /// ключа пустую строку, а в качестве сообщения об ошибке — значение 
        /// параметра <paramref name="message"/>.
        /// </summary>
        /// <param name="context">Экземпляр валидационного контекста.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        public static void AddError(this IValidationContext context, string message)
        {
            context.AddError(String.Empty, message);
        }

        /// <summary>
        /// Добавляет указанный префикс ко всем ключам ошибок, добавленных в границах
        /// действия данного вызова метода, используя метод <see cref="IValidationContext.ModifyKeys"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Замена производится по следующим правилам (в качестве примера возьмем префикс "User"):
        /// 
        /// * Пустой ключ будет заменен на префикс: {empty} -> User
        /// * К индексу будет добавлен префикс: [3] -> User[3]
        /// * К индексу с указанным свойством будет добавлен префикс: [3].Name -> User[3].Name
        /// * К непустому ключу, значение которого не начинается с индекса, будет 
        /// добавлен префикс: Name -> User.Name
        /// </remarks>
        /// 
        /// <param name="context">Экземпляр валидационного контекста.</param>
        /// <param name="name">Префикс к ключам ошибок.</param>
        /// <returns>Объект, метод <c>Dispose</c> которого нужно вызвать для выхода из границ замены.</returns>
        public static IDisposable PrefixErrorKeysWithName(this IValidationContext context, string name)
        {
            var emptyReplace = context.ModifyKeys(@"^((\[([\d]+)\]))?$", name + "$1");
            var indexReplace = context.ModifyKeys(@"^((\[[\d]\]+(\.)?))(.+)$", String.Format("{0}$1$4", name));
            var nonEmptyReplace = context.ModifyKeys(@"^(?:(?!\[([\d]+)\]))(.+)$", string.Format("{0}.$2", name));
            return new DisposableAction(() =>
                {
                    emptyReplace.Dispose();
                    indexReplace.Dispose();
                    nonEmptyReplace.Dispose();
                });
        }
    }
}
