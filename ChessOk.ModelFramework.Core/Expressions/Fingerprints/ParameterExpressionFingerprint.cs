// -----------------------------------------------------------------------
// <copyright file="ParameterExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class ParameterExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public int ParameterIndex { get; private set; }

        public ParameterExpressionFingerprint(ExpressionType nodeType, Type type, int parameterIndex)
            : base(nodeType, type)
        {
            ParameterIndex = parameterIndex;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as ParameterExpressionFingerprint;
            if (expressionFingerprint != null && ParameterIndex == expressionFingerprint.ParameterIndex)
                return Equals(expressionFingerprint);
            return false;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddInt32(ParameterIndex);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}
