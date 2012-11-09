using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Занимается преобразованием ключей валидационных
    /// ошибок, а также их нормализацией.
    /// </summary>
    public class ValidationErrorKeyTransformer
    {
        private readonly Stack<ReplaceOptions> _replaceStack = new Stack<ReplaceOptions>();

        /// <summary>
        /// Нормализует ключ валидационной ошибки.
        /// </summary>
        /// <param name="key">Ключ ошибки.</param>
        /// <returns>Нормализованный ключ.</returns>
        public string NormalizeKey(string key)
        {
            return key ?? String.Empty;
        }

        /// Нормализует ключ и, если задана модификация ключей путем однократного или 
        /// многократного вызова метода <see cref="ModifyKeys"/>, то имя ключа будет 
        /// изменено в соответствии со всеми заменами.
        public string NormalizeKeyAndApplyReplaces(string key)
        {
            key = NormalizeKey(key);

            var replaces = _replaceStack.ToArray();
            foreach (var replace in replaces)
            {
                key = replace.Regex.Replace(key, replace.Replacement);
            }

            return key;
        }

        /// <summary>
        /// Задает правила преобразования ключей, новое значение которого будет
        /// возвращено в <see cref="NormalizeKeyAndApplyReplaces"/> на основе
        /// замены по регулярному выражению.
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
        /// using (var emptyReplace = transformer.ModifyKeys(@"^$", name))
        /// using (var propReplace = transformer.ModifyKeys(@"^(.+)$", name + ".$1"))
        /// {
        ///     Console.WriteLine(transformer.NormalizeKeyAndApplyReplaces("")); // User
        ///     Console.WriteLine(transformer.NormalizeKeyAndApplyReplaces("Name"); //User.Name
        /// }
        /// 
        /// // За пределами using замена не производится
        /// Console.WriteLine(transformer.NormalizeKeyAndApplyReplaces("")); // Пустая строка
        /// </code>
        /// 
        /// </example>
        /// 
        /// <param name="pattern">Регулярное выражение.</param>
        /// <param name="replacement">Строка замены.</param>
        /// <returns>Объект, вызов <c>Dispose</c> которого определяет границу замены ключей.</returns>
        public IDisposable ModifyKeys(string pattern, string replacement)
        {
            _replaceStack.Push(new ReplaceOptions
                {
                    Regex = new Regex(pattern),
                    Replacement = replacement
                });

            return new DisposableAction(() => _replaceStack.Pop());
        }

        private struct ReplaceOptions
        {
            public Regex Regex { get; set; }
            public string Replacement { get; set; }
        }
    }
}