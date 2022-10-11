using System;

namespace Dot.Framework
{
    internal class ControllerPool : ObjectPoolSet
    {
        public ObjectPool CreatePool<T>() where T : IController, new()
        {
            return CreatePool(typeof(T));
        }

        private ObjectPool CreatePool(Type type)
        {
            var pool = GetPool(type);
            if (pool != null)
            {
                return pool;
            }

            pool = CreatePool(
                    type,
                    () =>
                    {
                        return Activator.CreateInstance(type);
                    },
                    (item) =>
                    {
                        ((IController)item).Reset();
                    }
                );
            return pool;
        }

        public T GetController<T>(bool createIfNot = true) where T : IController, new()
        {
            var type = typeof(T);
            if (!HasPool(type) && createIfNot)
            {
                CreatePool<T>();
            }

            return (T)Get(type);
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
