using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Autofac;

namespace ChessOk.ModelFramework.Validation
{
    /// <summary>
    /// Предоставляет реализацию интерфейса <see cref="IValidationContext"/>,
    /// инициализируемую внутри <see cref="IModelContext"/>.
    /// </summary>
    public class ValidationContext : IValidationContext
    {
        private readonly IModelContext _context;

        private readonly IDictionary<string, IList<string>> _errors =
            new Dictionary<string, IList<string>>();

        private readonly Stack<ReplaceOptions> _replaceStack = new Stack<ReplaceOptions>();

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ValidationContext"/>,
        /// используя указанный <paramref name="parentContext"/>.
        /// </summary>
        /// <param name="parentContext"></param>
        public ValidationContext(IModelContext parentContext)
        {
            _context = parentContext.CreateChildContext(
                ContextHierarchy.ValidationContext,
                x => x.RegisterInstance(this).As<IValidationContext>());
        }

        public bool IsValid
        {
            get { return _errors.Count == 0; }
        }

        public ICollection<string> this[string key]
        {
            get
            {
                return GetErrors(key);
            }
        }
        
        public ICollection<string> Keys
        {
            get
            {
                return _errors.Keys;
            }
        }

        public IModelContext Context
        {
            get
            {
                return _context;
            }
        }

        public void AddError(string key, string message)
        {
            key = NormalizeKey(key);
            key = ApplyReplaces(key);

            if (!_errors.ContainsKey(key))
            {
                _errors.Add(key, new List<string>());
            }

            _errors[key].Add(message);
        }

        public void RemoveErrors(string key)
        {
            key = NormalizeKey(key);
            _errors.Remove(key);
        }

        public void Clear()
        {
            _errors.Clear();
        }

        public ICollection<string> GetErrors(string key)
        {
            key = NormalizeKey(key);

            return !_errors.ContainsKey(key) ? new string[0] : _errors[key];
        }

        public void ThrowExceptionIfInvalid()
        {
            if (!IsValid) { throw new ValidationException(this); }
        }

        public IDisposable ModifyKeys(string pattern, string replacement)
        {
            _replaceStack.Push(new ReplaceOptions
            {
                Regex = new Regex(pattern),
                Replacement = replacement
            });

            return new DisposableAction(() => _replaceStack.Pop());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private string ApplyReplaces(string key)
        {
            var replaces = _replaceStack.ToArray();
            foreach (var replace in replaces)
            {
                key = replace.Regex.Replace(key, replace.Replacement);
            }

            return key;
        }

        private static string NormalizeKey(string key)
        {
            return key ?? string.Empty;
        }

        private struct ReplaceOptions
        {
            public Regex Regex { get; set; }
            public string Replacement { get; set; }
        }
    }
}