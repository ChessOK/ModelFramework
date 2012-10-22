using System;

namespace ChessOk.ModelFramework
{
    public class Entity<TId> : IEquatable<Entity<TId>>
    {
        public virtual TId Id { get; protected set; }

        public virtual bool IsTransient
        {
            get { return Id.Equals(default(TId)); }
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (!this.IsTransient && !other.IsTransient && Equals(Id, other.Id))
            {
                var otherType = other.GetType();
                var thisType = this.GetType();

                return thisType.IsAssignableFrom(otherType) ||
                otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override bool Equals(object other)
        {
            return Equals(other as Entity<TId>);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 397 ^ GetType().GetHashCode();
        }

        public virtual Type GetUnproxiedType()
        {
            return GetType();
        }
    }
}