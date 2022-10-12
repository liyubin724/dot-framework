using System;

namespace Dot.Framework
{
    public class ControllerPool : ObjectPoolSet
    {
        public ObjectPool CreatePool(Type type)
        {
            var pool = GetPool(type);
            if (pool != null)
            {
                return pool;
            }

            Func<object> create = () =>
            {
                var controller = (IController)Activator.CreateInstance(type);
                controller?.Initialize();
                return controller;
            };

            Action<object> destroy = (controller) =>
            {
                ((IController)controller).Destroy();
            };

            pool = CreatePool(type, create, null, destroy);

            return pool;
        }

        public ObjectPool CreatePool<T>() where T : IController, new()
        {
            return CreatePool(typeof(T));
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
