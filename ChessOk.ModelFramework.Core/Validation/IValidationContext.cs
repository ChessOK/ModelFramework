using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Scopes;

namespace ChessOk.ModelFramework.Validation
{
    public interface IValidationContext : IDisposable
    {
        IModelScope Model { get; }
        bool IsValid { get; }

        ICollection<string> this[string key] { get; }
        ICollection<string> Keys { get; }

        void AddError(string key, string message);
        ICollection<string> GetErrors(string key); 
        void RemoveErrors(string key);
        void Clear();

        IDisposable ModifyKeys(string pattern, string replacement);

        void ThrowExceptionIfInvalid();
    }
}
