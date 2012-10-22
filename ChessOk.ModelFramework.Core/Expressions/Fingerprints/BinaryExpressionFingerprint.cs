// -----------------------------------------------------------------------
// <copyright file="BinaryExpressionFingerprint.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace ChessOk.ModelFramework.Expressions
{
    [SuppressMessage("Microsoft.Usage", "CA2218:OverrideGetHashCodeOnOverridingEquals", Justification = "Overrides AddToHashCodeCombiner() instead.")]
#pragma warning disable 0659
    internal sealed class BinaryExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public MethodInfo Method { get; private set; }

        public BinaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method)
            : base(nodeType, type)
        {
            Method = method;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as BinaryExpressionFingerprint;
            if (expressionFingerprint != null && Equals(this.Method, expressionFingerprint.Method))
                return Equals(expressionFingerprint);
            return false;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Method);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}
