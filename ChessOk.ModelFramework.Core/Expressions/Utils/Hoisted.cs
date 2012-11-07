// -----------------------------------------------------------------------
// <copyright file="Hoisted.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ChessOk.ModelFramework.Expressions
{
    internal delegate TValue Hoisted<in TModel, out TValue>(TModel model, List<object> capturedConstants);
}
