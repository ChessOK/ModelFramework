// -----------------------------------------------------------------------
// <copyright file="TypeBinaryExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class TypeBinaryExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public Type TypeOperand { get; private set; }

        public TypeBinaryExpressionFingerprint(ExpressionType nodeType, Type type, Type typeOperand)
            : base(nodeType, type)
        {
            TypeOperand = typeOperand;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as TypeBinaryExpressionFingerprint;
            if (expressionFingerprint != null && TypeOperand == expressionFingerprint.TypeOperand)
                return Equals(expressionFingerprint);
            return false;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(TypeOperand);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}
