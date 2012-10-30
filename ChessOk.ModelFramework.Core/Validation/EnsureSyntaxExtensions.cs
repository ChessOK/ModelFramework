using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ChessOk.ModelFramework.Validation.Validators;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет основные расширения для <see cref="IEnsureSyntax{TObject}"/>
    /// </summary>
    public static class EnsureSyntaxExtensions
    {
        /// <summary>
        /// Проверить текущий объект, используя его валидационные атрибуты и вызвать <see cref="IValidatable.Validate"/>,
        /// если объект реализует интерфейс <see cref="IValidatable"/>. Валидация осуществляется с помощью <see cref="ObjectValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="usePropertyNamesAsErrorKeys">
        ///   Будет ли валидатор <see cref="AttributesValidator"/> использовать имена свойств в качестве ключей ошибок.
        /// </param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsValid<T>(this IEnsureSyntax<T> syntax, bool usePropertyNamesAsErrorKeys = true)
        {
            var validator = syntax.ValidationContext.Model.Get<ObjectValidator>();
            validator.AttributesValidator.UsePropertyNamesAsErrorKeys = usePropertyNamesAsErrorKeys;
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что длина текущей строки меньше, либо равна числу символов <paramref name="maximumLength"/>. 
        /// Валидация осуществляется с помощью <see cref="MaxLengthValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="maximumLength">Максимальная длина проверяемой строки.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<string> HasMaxLength(this IEnsureSyntax<string> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что длина текущего массива меньше, либо равна <paramref name="maximumLength"/>. 
        /// Валидация осуществляется с помощью <see cref="MaxLengthValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="maximumLength">Максимальная длина проверяемого массива.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T[]> HasMaxLength<T>(this IEnsureSyntax<T[]> syntax, int maximumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MaxLengthValidator>();
            validator.Length = maximumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что длина текущей строки больше, либо равна <paramref name="minimumLength"/> символов. 
        /// Валидация осуществляется с помощью <see cref="MaxLengthValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="minimumLength">Минимальная длина проверяемой строки.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<string> HasMinLength(this IEnsureSyntax<string> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что длина текущего массива больше, либо равна <paramref name="minimumLength"/>. 
        /// Валидация осуществляется с помощью <see cref="MaxLengthValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="minimumLength">Минимальная длина проверяемого массива.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T[]> HasMinLength<T>(this IEnsureSyntax<T[]> syntax, int minimumLength, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<MinLengthValidator>();
            validator.Length = minimumLength;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что дата является корректной с точки зрения MS SQLServer и типа данных datetime.
        /// Валидация осуществляется с помощью <see cref="SqlDateTimeValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<DateTime> IsSqlDateTime(this IEnsureSyntax<DateTime> syntax, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<SqlDateTimeValidator>();
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что значение объекта не равно <c>null</c>, либо не равно пустой строке, 
        /// если задан <paramref name="allowEmptyStrings"/>.
        /// Валидация осуществляется с помощью <see cref="RequiredValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <param name="allowEmptyStrings">Разрешать ли пустые строки.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsRequired<T>(this IEnsureSyntax<T> syntax, string message = null, bool allowEmptyStrings = false)
            where T : class
        {
            var validator = syntax.ValidationContext.Model.Get<RequiredValidator>();
            validator.AllowEmptyStrings = allowEmptyStrings;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что значение объекта не равно <c>null</c>.
        /// Валидация осуществляется с помощью <see cref="RequiredValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="message">Опциональное сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T?> IsRequired<T>(this IEnsureSyntax<T?> syntax, string message = null)
            where T : struct
        {
            var validator = syntax.ValidationContext.Model.Get<RequiredValidator>();
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что значение объекта равно <c>null</c>.
        /// Валидация осуществляется с помощью <see cref="NullValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsNull<T>(this IEnsureSyntax<T> syntax)
            where T : class
        {
            var validator = syntax.ValidationContext.Model.Get<NullValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что значение объекта равно <c>null</c>.
        /// Валидация осуществляется с помощью <see cref="NullValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T?> IsNull<T>(this IEnsureSyntax<T?> syntax)
            where T : struct
        {
            var validator = syntax.ValidationContext.Model.Get<NullValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что делегат <paramref name="@delegate"/> возвращает значение <c>True</c>.
        /// Валидация осуществляется с помощью <see cref="DelegateValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="delegate">Делегат, выполняющий проверку.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsTrue<T>(this IEnsureSyntax<T> syntax, Func<T, bool> @delegate,  string message)
        {
            var validator = syntax.ValidationContext.Model.Get<DelegateValidator>();
            validator.Delegate = obj => @delegate((T)obj);
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что строковое представление объекта полностью совпадает с регулярным выражением <paramref name="pattern"/>.
        /// Валидация осуществляется с помощью <see cref="RegularExpressionValidator"/>.
        /// </summary>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="pattern">Регулярное выражение.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<string> Matches(this IEnsureSyntax<string> syntax, string pattern, string message = null)
        {
            var validator = syntax.ValidationContext.Model.Get<RegularExpressionValidator>();
            validator.Pattern = pattern;
            validator.Message = message;

            return syntax.IsValid(validator);
        }

        #region CollectionValidator

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<IEnumerable<T>> HasValidItems<T>(this IEnsureSyntax<IEnumerable<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T[]> HasValidItems<T>(this IEnsureSyntax<T[]> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<List<T>> HasValidItems<T>(this IEnsureSyntax<List<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<IList<T>> HasValidItems<T>(this IEnsureSyntax<IList<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<Collection<T>> HasValidItems<T>(this IEnsureSyntax<Collection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        /// <summary>
        /// Убедиться, что все элементы коллекции являются корректными. Валидация происходит на основе
        /// <see cref="CollectionValidator"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<ICollection<T>> HasValidItems<T>(this IEnsureSyntax<ICollection<T>> syntax)
        {
            var validator = syntax.ValidationContext.Model.Get<CollectionValidator>();
            return syntax.IsValid(validator);
        }

        #endregion

        /// <summary>
        /// Заявить, что данный объект не является корректным с заданным в параметре <paramref name="message"/> 
        /// сообщением об ошибке.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsNotValid<T>(this IEnsureSyntax<T> syntax, string message)
        {
            return syntax.IsTrue(x => false, message);
        }

        /// <summary>
        /// Заявить, что данный объект является некорректным, если удовлетворено условие,
        /// заданное в параметре <paramref name="condition"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="condition">Проверяемое условие.</param>
        /// <param name="message">Сообщение об ошибкею</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsNotValidIf<T>(this IEnsureSyntax<T> syntax, bool condition, string message)
        {
            return syntax.IsTrue(x => condition == false, message);
        }

        /// <summary>
        /// Заявить, что данный объект является некорректным, если объект <paramref name="obj"/> равен <c>null</c>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="obj">Объект, который не должен быть равен null.</param>
        /// <param name="message">Сообщение об ошибкею</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsNotValidIfNull<T>(this IEnsureSyntax<T> syntax, object obj, string message)
        {
            return syntax.IsNotValidIf(obj == null, message);
        }

        /// <summary>
        /// Заявить, что данный объект является корректным, если удовлетворено условие <paramref name="condition"/>.
        /// В противном случае в контекст добавляется сообщение об ошибке <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="condition">Проверяемое условие.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsValidIf<T>(this IEnsureSyntax<T> syntax, bool condition, string message)
        {
            return syntax.IsTrue(x => condition, message);
        }

        /// <summary>
        /// Убедиться, что значение объекта больше, чем значение объекта, указанного в параметре <paramref name="other"/>.
        /// в противном случае добавить валидационную ошибку с сообщением <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="other">Объект, с которым сравнивается текущий.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsGreaterThan<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) < 0, message);
        }

        /// <summary>
        /// Убедиться, что значение объекта больше, либо равно значению объекта, указанному в параметре <paramref name="other"/>.
        /// в противном случае добавить валидационную ошибку с сообщением <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="other">Объект, с которым сравнивается текущий.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsGreaterThanOrEqual<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) <= 0, message);
        }

        /// <summary>
        /// Убедиться, что значение объекта меньше, чем значение объекта, указанного в параметре <paramref name="other"/>.
        /// в противном случае добавить валидационную ошибку с сообщением <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="other">Объект, с которым сравнивается текущий.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsLessThan<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) > 0, message);
        }

        /// <summary>
        /// Убедиться, что значение объекта меньше, либо равно значению объекта, указанному в параметре <paramref name="other"/>.
        /// в противном случае добавить валидационную ошибку с сообщением <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="syntax">Синтаксис.</param>
        /// <param name="other">Объект, с которым сравнивается текущий.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Синтаксис, ассоциированный с исходным объектом.</returns>
        public static IEnsureSyntax<T> IsLessThanOrEqual<T>(
            this IEnsureSyntax<T> syntax, IComparable<T> other, string message)
        {
            if (other == null)
            {
                return syntax;
            }

            return syntax.IsTrue(x => other.CompareTo(x) >= 0, message);
        }
    }
}