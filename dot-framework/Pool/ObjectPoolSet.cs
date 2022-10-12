using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    internal class ObjectPoolSet
    {
        private Dictionary<Type, ObjectPool> m_PoolDic = new Dictionary<Type, ObjectPool>();

        public bool HasPool(Type type)
        {
            return m_PoolDic.ContainsKey(type);
        }

        public ObjectPool CreatePool(Type type, Func<object> createFunc, Action<object> resetAction = null, Action<object> destroyAction = null)
        {
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                return pool;
            }

            pool = new ObjectPool(createFunc)
            {
                ResetAction = resetAction,
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

    //internal class ObjectPoolSet<T> where T : class
    //{
    //    private Dictionary<Type, ObjectPool<T>> m_PoolDic = new Dictionary<Type, ObjectPool<T>>();

    //    public bool HasPool<I>() where I : T
    //    {
    //        return m_PoolDic.ContainsKey(typeof(I));
    //    }

    //    public ObjectPool<T> CreatePool<I>(Func<I> createFunc, Action<I> resetAction) where I : T
    //    {
    //        Type type = typeof(I);
    //        if (m_PoolDic.TryGetValue(type, out var pool))
    //        {
    //            return pool;
    //        }

    //        pool = new ObjectPool<T>(() =>
    //        {
    //            return createFunc();
    //        }, (item) =>
    //        {
    //            resetAction((I)item);
    //        });
    //        return pool;
    //    }

    //    public ObjectPool<T> GetPool<I>() where I : T
    //    {
    //        if (m_PoolDic.TryGetValue(typeof(I), out var pool))
    //        {
    //            return pool;
    //        }

    //        return null;
    //    }

    //    public void DestroyPool<I>() where I : T
    //    {
    //        if (m_PoolDic.TryGetValue(typeof(I), out var pool))
    //        {
    //            m_PoolDic.Remove(typeof(I));

    //            pool.Clear();
    //        }
    //    }

    //    public I Get<I>() where I : T
    //    {
    //        var pool = GetPool<I>();
    //        if (pool != null)
    //        {
    //            return (I)pool.Get();
    //        }
    //        return default;
    //    }

    //    public void Release<I>(I item) where I : T
    //    {
    //        var pool = GetPool<I>();
    //        if (pool != null)
    //        {
    //            pool.Release(item);
    //        }
    //    }

    //    public void ClearAll()
    //    {
    //        foreach (var kvp in m_PoolDic)
    //        {
    //            kvp.Value.Clear();
    //        }
    //        m_PoolDic.Clear();
    //    }

    //}
}
