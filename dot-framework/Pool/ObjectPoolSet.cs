using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public class ObjectPoolSet
    {
        private Dictionary<Type, ObjectPool> m_PoolDic = new Dictionary<Type, ObjectPool>();

        public bool HasPool(Type type)
        {
            return m_PoolDic.ContainsKey(type);
        }

        public ObjectPool CreatePool(
            Type type, 
            Func<object> createFunc, 
            Action<object> retainAction = null,
            Action<object> releaseAction = null,
            Action<object> destroyAction = null)
        {
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                return pool;
            }

            pool = new ObjectPool(createFunc)
            {
                RetainAction = retainAction,
                ReleaseAction = releaseAction,
                DestroyAction = destroyAction
            };

            m_PoolDic.Add(type, pool);

            return pool;
        }

        public void DestroyPool(Type type)
        {
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                m_PoolDic.Remove(type);

                pool.Clear();
            }
        }

        public ObjectPool GetPool(Type type)
        {
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                return pool;
            }

            return null;
        }

        public object Get(Type type)
        {
            var pool = GetPool(type);
            if (pool == null)
            {
                return null;
            }

            return pool.Get();
        }

        public T Get<T>() where T : class
        {
            return (T)Get(typeof(T));
        }

        public void Release(object item)
        {
            if (item == null)
            {
                return;
            }
            Type type = item.GetType();
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                pool.Release(item);
            }
        }

        public void Clear()
        {
            foreach (var kvp in m_PoolDic)
            {
                kvp.Value.Clear();
            }
            m_PoolDic.Clear();
        }
    }
}
