using System.Collections.Generic;

namespace Dot.Framework
{
    public class EntityEqualityComparer<TEntity> : IEqualityComparer<TEntity> where TEntity : class, IEntity
    {
        public bool Equals(TEntity x, TEntity y)
        {
            if (x == y) return true;
            if (x.Id == y.Id) return true;

            return false;
        }

        public int GetHashCode(TEntity obj)
        {
            return obj.Id;
        }
    }
}
