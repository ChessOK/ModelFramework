// -----------------------------------------------------------------------
// <copyright file="HashCodeCombiner.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;

namespace ChessOk.ModelFramework.Expressions
{
    internal class HashCodeCombiner
    {
        private long _combinedHash64 = 5381L;

        public int CombinedHash
        {
            get
            {
                return _combinedHash64.GetHashCode();
            }
        }

        public void AddFingerprint(ExpressionFingerprint fingerprint)
        {
            if (fingerprint != null)
                fingerprint.AddToHashCodeCombiner(this);
            else
                AddInt32(0);
        }

        public void AddEnumerable(IEnumerable e)
        {
            if (e == null)
            {
                AddInt32(0);
            }
            else
            {
                int i = 0;
                foreach (object o in e)
                {
                    AddObject(o);
                    ++i;
                }
                AddInt32(i);
            }
        }

        public void AddInt32(int i)
        {
            _combinedHash64 = (_combinedHash64 << 5) + _combinedHash64 ^ i;
        }

        public void AddObject(object o)
        {
            AddInt32(o != null ? o.GetHashCode() : 0);
        }
    }
}
