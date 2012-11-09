// -----------------------------------------------------------------------
// <copyright file="MethodCallExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class MethodCallExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public MethodInfo Method { get; private set; }

        public MethodCallExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method)
            : base(nodeType, type)
        {
            Method = method;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as MethodCallExpressionFingerprint;
            if (expressionFingerprint != null && Equals(Method, expressionFingerprint.Method))
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
