using System;
using System.Linq.Expressions;

using ChessOk.ModelFramework.Validation.Internals;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет интерфейс для валидации объекта типа <typeparamref name="TObject"/>,
    /// используя расширяемый Fluent-синтаксис.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IEnsureSyntax<TObject>
    {
        /// <summary>
        /// Получает ассоциированный с синтаксисом экземпляр
        /// <see cref="IValidationContext"/>.
        /// </summary>
        IValidationContext ValidationContext { get; }

        /// <summary>
        /// Произвести валидацию исходного объекта с помощью валидатора, указанного в параметре
        /// <paramref name="validator"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// При этом указанный <paramref name="validator"/> ассоциируется с текущим
        /// <see cref="ValidationContext"/> и вызывает его метод <see cref="IValidator.Validate"/>.
        /// </remarks>
        /// 
        /// <param name="validator">Экземпляр валидатора.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ассоциированный с исходным объектом.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="validator"/> не задан.</exception>
        IEnsureSyntax<TObject> IsValid(IValidator validator);

        /// <summary>
        /// Произвести валидацию указанного через лямбда-выражение <paramref name="propertyExpression"/>
        /// свойства. Инструкции по валидации указываются в параметре <paramref name="validation"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// При этом создается новый экземпляр <see cref="EnsureEngine{TProperty}"/>, ассоциированный
        /// со значением указанного свойства.
        /// 
        /// Если исходный объект был равен <c>null</c>, то делегат <paramref name="validation"/>
        /// не вызывается.
        /// 
        /// Для всех ключей ошибок, устанавливаемых в делегате <paramref name="validation"/> добавляется
        /// префикс с именем свойства, используя <see cref="ValidationContextExtensions.PrefixErrorKeysWithName"/>.
        /// </remarks>
        /// 
        /// <typeparam name="TProperty">Тип свойства.</typeparam>
        /// <param name="propertyExpression">Лямбда-выражение свойства.</param>
        /// <param name="validation">Делегат с инструкциями по валидации свойства.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ассоциированный с исходным объектом (не свойством).</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="propertyExpression"/> не задан.</exception>
        /// <exception cref="ArgumentNullException">Делегат <paramref name="validation"/> не задан.</exception>
        IEnsureSyntax<TObject> ItsProperty<TProperty>(
            Expression<Func<TObject, TProperty>> propertyExpression,
            Action<IEnsureSyntax<TProperty>> validation);

        /// <summary>
        /// Произвести валидацию свойства с именем <paramref name="propertyName"/>
        /// Инструкции по валидации указываются в параметре <paramref name="validation"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// При этом создается новый экземпляр <see cref="EnsureEngine{TProperty}"/>, ассоциированный
        /// со значением указанного свойства.
        /// 
        /// Если исходный объект был равен <c>null</c>, то делегат <paramref name="validation"/>
        /// не вызывается.
        /// 
        /// Для всех ключей ошибок, устанавливаемых в делегате <paramref name="validation"/> добавляется
        /// префикс с именем свойства, используя <see cref="ValidationContextExtensions.PrefixErrorKeysWithName"/>.
        /// </remarks>
        /// 
        /// <typeparam name="TProperty">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя валидируемого свойства.</param>
        /// <param name="validation">Делегат с инструкциями по валидации свойства.</param>
        /// <returns><see cref="IEnsureSyntax{TObject}"/>, ассоциированный с исходным объектом (не свойством).</returns>
        /// <exception cref="InvalidOperationException">Свойство с именем <paramref name="propertyName"/> не найдено.</exception>
        /// <exception cref="ArgumentNullException">Делегат <paramref name="validation"/> не задан.</exception>
        IEnsureSyntax<TObject> ItsProperty<TProperty>(
            string propertyName,
            Action<IEnsureSyntax<TProperty>> validation);
    }
}