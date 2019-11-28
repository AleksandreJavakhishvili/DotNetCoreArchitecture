using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreArchitecture.SeedWork
{
    public abstract class Entity<TEntity> : EntityBase<TEntity, int>
    {
        private int? _requestedHashCode;

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TEntity>))
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (Entity<TEntity>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(Entity<TEntity> left, Entity<TEntity> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity<TEntity> left, Entity<TEntity> right)
        {
            return !(left == right);
        }
    }
}
