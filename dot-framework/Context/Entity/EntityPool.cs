using System;

namespace Dot.Framework
{
    internal class EntityPool : ObjectPoolSet
    {
        public ObjectPool CreatePool<E>() where E : IEntity, new()
        {
            return CreatePool(typeof(E));
        }

        public ObjectPool CreatePool(Type type)
        {
            var pool = GetPool(type);
            if (pool != null)
            {
                return pool;
            }

            Func<object> create = () =>
            {
                var entity = (IEntity)Activator.CreateInstance(type);
                entity.Initialize();
                return entity;
            };

            Action<object> reset = (entity) =>
            {

            };

            pool = CreatePool(type, create, reset, null);
            return pool;
        }

        public E GetEntity<E>(bool createIfNot = true) where E : IEntity, new()
        {
            var type = typeof(E);
            if (!HasPool(type) && createIfNot)
            {
                CreatePool<E>();
            }

            return (E)Get(type);
        }

        public void ReleaseController(IController item, bool createIfNot = false)
        {
            var type = item.GetType();
            if (!HasPool(type) && createIfNot)
            {
                CreatePool(type);
            }

            Release(item);
        }
    }
}
