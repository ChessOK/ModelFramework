// -----------------------------------------------------------------------
// <copyright file="ExpressionFingerprintChain.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessOk.ModelFramework.Expressions
{
    internal sealed class ExpressionFingerprintChain : IEquatable<ExpressionFingerprintChain>
    {
        public readonly List<ExpressionFingerprint> Elements = new List<ExpressionFingerprint>();

        public bool Equals(ExpressionFingerprintChain other)
        {
            if (other == null || Elements.Count != other.Elements.Count)
                return false;

            return !Elements.Where((t, index) => !Equals(t, other.Elements[index])).Any();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExpressionFingerprintChain);
        }

        public override int GetHashCode()
        {
            var hashCodeCombiner = new HashCodeCombiner();
            Elements.ForEach(hashCodeCombiner.AddFingerprint);
            return hashCodeCombiner.CombinedHash;
        }
    }
}
