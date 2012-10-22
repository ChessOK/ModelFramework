// -----------------------------------------------------------------------
// <copyright file="IndexExpressionFingerprint.cs" company="Microsoft">
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
    internal sealed class IndexExpressionFingerprint : ExpressionFingerprint
#pragma warning restore 0659
    {
        public PropertyInfo Indexer { get; private set; }

        public IndexExpressionFingerprint(ExpressionType nodeType, Type type, PropertyInfo indexer)
            : base(nodeType, type)
        {
            Indexer = indexer;
        }

        public override bool Equals(object obj)
        {
            var expressionFingerprint = obj as IndexExpressionFingerprint;
            if (expressionFingerprint != null && Equals(Indexer, expressionFingerprint.Indexer))
                return Equals(expressionFingerprint);
            return false;
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Indexer);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}
