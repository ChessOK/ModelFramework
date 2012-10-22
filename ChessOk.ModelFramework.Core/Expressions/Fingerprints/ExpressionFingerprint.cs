// -----------------------------------------------------------------------
// <copyright file="ExpressionFingerprint.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace ChessOk.ModelFramework.Expressions
{
    internal abstract class ExpressionFingerprint
    {
        public ExpressionType NodeType { get; private set; }

        public Type Type { get; private set; }

        protected ExpressionFingerprint(ExpressionType nodeType, Type type)
        {
            NodeType = nodeType;
            Type = type;
        }

        internal virtual void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddInt32((int)NodeType);
            combiner.AddObject(Type);
        }

        protected bool Equals(ExpressionFingerprint other)
        {
            if (other != null && NodeType == other.NodeType)
                return Equals(Type, other.Type);
            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExpressionFingerprint);
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();
            AddToHashCodeCombiner(combiner);
            return combiner.CombinedHash;
        }
    }
}
