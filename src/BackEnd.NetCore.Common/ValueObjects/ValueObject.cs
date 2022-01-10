using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.NetCore.Common.ValueObjects
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public virtual bool Equals(ValueObject other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) ||
                   GetEqualityComponents()
                       .SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ValueObject)obj);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public abstract object GetValue();

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
