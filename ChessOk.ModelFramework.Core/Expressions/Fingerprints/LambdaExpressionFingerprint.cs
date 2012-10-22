// -----------------------------------------------------------------------
// <copyright file="LambdaExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class LambdaExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public LambdaExpressionFingerprint(ExpressionType nodeType, Type type)
            : base(nodeType, type)
        {
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as LambdaExpressionFingerprint;
            return expressionFingerprint != null && Equals(expressionFingerprint);
        }
    }
}
