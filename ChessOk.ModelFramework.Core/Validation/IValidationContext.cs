using System;
using System.Collections.Generic;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет базовое API для операций, связанных
    /// с валидацией.
    /// </summary>
    public interface IValidationContext : IDisposable
    {
        IModelContext Context { get; }

        /// <summary>
        /// Регистрирует валидационную ошибку с ключом <paramref name="key"/>
        /// и сообщением <paramref name="message"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если в качестве ключа указано значение <c>null</c>, то оно будет
        /// преобразовано в пустую строку.
        /// 
        /// Если задана модификация ключей путем однократного или многократного 
        /// вызова метода <see cref="ModifyKeys"/>, то имя ключа будет изменено 
        /// в соответствии со всеми заменами.
        /// </remarks>
        /// 
        /// <param name="key">Ключ ошибки.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        void AddError(string key, string message);

        /// <summary>
        /// Получает значение, определяющее наличие валидационных ошибок 
        /// в текущем экземпляре класса, реализующего <see cref="IValidationContext"/>.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Бросает исключение <see cref="ValidationException"/>, если
        /// в контексте была зарегистрирована хотя бы одна ошибка.
        /// </summary>
        void ThrowExceptionIfInvalid();

        /// <summary>
        /// Получает коллекцию сообщений об ошибках по заданному ключу <paramref name="key"/>,
        /// зарегистрированных в текущем экземпляре класса, реализующего <see cref="IValidationContext"/>.
        /// </summary>
        /// <param name="key">Ключ ошибки.</param>
        /// <returns>Коллекция сообщений об ошибках.</returns>
        ICollection<string> this[string key] { get; }

        /// <summary>
        /// Получает коллекцию сообщений об ошибках по заданному ключу <paramref name="key"/>,
        /// зарегистрированных в текущем экземпляре класса, реализующего <see cref="IValidationContext"/>.
        /// </summary>
        /// <param name="key">Ключ ошибки.</param>
        /// <returns>Коллекция сообщений об ошибках.</returns>
        ICollection<string> GetErrors(string key);

        /// <summary>
        /// Получает коллекцию ключей ошибок, зарегистрированных
        /// в текущем экземпляре класса, реализующего <see cref="IValidationContext"/>.
        /// </summary>
        ICollection<string> Keys { get; }

        /// <summary>
        /// Удаляет все валидационные ошибки, зарегистрированные с ключом,
        /// указанном в параметре <paramref name="key"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Если в качестве имени ключа было задано значение <c>null</c>,
        /// то оно будет преобразовано в пустую строку.
        /// </remarks>
        /// 
        /// <param name="key">Ключ ошибок.</param>
        void RemoveErrors(string key);

        /// <summary>
        /// Удаляет все зарегистрированные валидационные ошибки в текущем
        /// экземпляре класса, реализующего <see cref="IValidationContext"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Задает границы, в пределах которых во время добавления валидационной ошибки 
        /// путем вызова метода <see cref="AddError"/>, будет произведена модификация
        /// ключа, путем его замены на основе регулярного выражения с параметрами,
        /// указанными при вызове данного метода.
        /// </summary>
        /// 
        /// <remarks>
        /// Поддерживаются вложенные замены, которые производятся на основе стека,
        /// т.е. первая добавленная замена будет выполнена последней.
        /// </remarks>
        /// 
        /// <example>
        /// Следующий код будет заменять все пустые ключи на ключ "User", а для непустых
        /// ключей будет добавляться префикс "User.".
        /// 
        /// <code lang="cs">
        /// using (var emptyReplace = context.ModifyKeys(@"^$", name))
        /// using (var propReplace = context.ModifyKeys(@"^(.+)$", name + ".$1"))
        /// {
        ///     context.AddError("", "Пользователь может быть зарегистрирован только администратором.");
        ///     context.AddError("Name", "Имя должно содержать менее 100 символов.");
        /// 
        ///     Console.WriteLine(context["User"].First()); // Выведет "Пользователь может..."
        ///     Console.WriteLine(context["User.Name"].First()); // Выведет "Имя должно..."
        /// }
        /// 
        /// context.AddError("", "Замена больше не производится");
        /// Console.WriteLine(context[""].First()); // Выведет "Замена больше..."
        /// </code>
        /// 
        /// </example>
        /// 
        /// <param name="pattern">Регулярное выражение.</param>
        /// <param name="replacement">Строка замены.</param>
        /// <returns>Объект, вызов <c>Dispose</c> которого определяет границу замены ключей.</returns>
        IDisposable ModifyKeys(string pattern, string replacement);
    }
}
