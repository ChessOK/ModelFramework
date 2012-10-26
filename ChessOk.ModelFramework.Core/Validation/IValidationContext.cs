using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Contexts;

namespace ChessOk.ModelFramework.Validation
{
    public interface IValidationContext : IContext
    {
        bool IsValid { get; }

        ICollection<string> this[string key] { get; }
        ICollection<string> Keys { get; }

        void AddError(string key, string message);
        void RemoveErrors(string key);
        void Clear();

        IDisposable ModifyKeys(string pattern, string replacement);

        void ThrowExceptionIfInvalid();
    }
}
