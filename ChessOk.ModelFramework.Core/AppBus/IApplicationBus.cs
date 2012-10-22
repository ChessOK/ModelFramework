using System;

using ChessOk.ModelFramework.Contexts;
using ChessOk.ModelFramework.Messages;
using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework
{
    public interface IApplicationBus : IDisposable
    {
        IValidationContext Validation { get; }
        IContext Context { get; }

        void Handle(IApplicationMessage message);
        bool TryHandle(IApplicationMessage message);
    }
}
