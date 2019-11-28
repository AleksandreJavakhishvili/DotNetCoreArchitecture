using System;

namespace DotNetCoreArchitecture.SeedWork
{
    public abstract class IntegrationalEntity<TEntity> : EntityBase<TEntity, Guid>
    {
        private int? _requestedHashCode;

        public override bool Equals(object obj)
        {
            if (!(obj is IntegrationalEntity<TEntity>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (IntegrationalEntity<TEntity>)obj;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }

        public static bool operator ==(IntegrationalEntity<TEntity> left, IntegrationalEntity<TEntity> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(IntegrationalEntity<TEntity> left, IntegrationalEntity<TEntity> right)
        {
            return !(left == right);
        }
    }
}
