using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Autofac;

using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework.Validation.Internals
{
    public class ValidationContext : IValidationContext
    {
        private readonly IModelScope _modelScope;

        private readonly IDictionary<string, IList<string>> _errors =
            new Dictionary<string, IList<string>>();

        private readonly Stack<ReplaceOptions> _replaceStack = new Stack<ReplaceOptions>();

        public ValidationContext(IModelScope parentContext)
        {
            _modelScope = new ModelScope(
                parentContext,
                ScopeHierarchy.ValidationContext,
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

        public IModelScope Model
        {
            get
            {
                return _modelScope;
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

            if (!_errors.ContainsKey(key))
            {
                return new string[0];
            }

            return _errors[key];
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
            _modelScope.Dispose();
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
            return (key ?? string.Empty);
        }

        private struct ReplaceOptions
        {
            public Regex Regex { get; set; }
            public string Replacement { get; set; }
        }
    }
}