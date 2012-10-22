// -----------------------------------------------------------------------
// <copyright file="MemberExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class MemberExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public MemberInfo Member { get; private set; }

        public MemberExpressionFingerprint(ExpressionType nodeType, Type type, MemberInfo member)
            : base(nodeType, type)
        {
            Member = member;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as MemberExpressionFingerprint;
            if (expressionFingerprint != null && Equals(Member, expressionFingerprint.Member))
                return Equals(expressionFingerprint);
            return false;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Member);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}
