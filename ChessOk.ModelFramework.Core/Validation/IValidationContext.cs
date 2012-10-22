﻿using System;
using System.Collections.Generic;

using ChessOk.ModelFramework.Contexts;

namespace ChessOk.ModelFramework.Validation
{
    public interface IValidationContext
    {
        bool IsValid { get; }

        ICollection<string> this[string key] { get; }
        ICollection<string> Keys { get; }

        IContext Context { get; }

        void AddError(string key, string message);
        void RemoveErrors(string key);
        void Clear();

        IDisposable ReplaceKeys(string pattern, string replacement);

        void ThrowExceptionIfInvalid();
    }
}
