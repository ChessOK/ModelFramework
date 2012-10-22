// -----------------------------------------------------------------------
// <copyright file="ConditionalExpressionFingerprint.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ChessOk.ModelFramework.Expressions
{
    [SuppressMessage("Microsoft.Usage", "CA2218:OverrideGetHashCodeOnOverridingEquals", Justification = "Overrides AddToHashCodeCombiner() instead.")]
#pragma warning disable 0659
    internal sealed class ConditionalExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public ConditionalExpressionFingerprint(ExpressionType nodeType, Type type)
            : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as ConditionalExpressionFingerprint;
            if (expressionFingerprint != null)
                return Equals(expressionFingerprint);
            return false;
        }
    }
}
