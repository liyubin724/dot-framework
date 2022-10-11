using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    internal class ObjectSetPool<T> where T : class
    {
        private Dictionary<Type, ObjectPool<T>> m_PoolDic = new Dictionary<Type, ObjectPool<T>>();

        public bool HasPool<I>() where I:T
        {
            return m_PoolDic.ContainsKey(typeof(I));
        }

        public virtual ObjectPool<T> CreatePool<I>(Func<I> creator, Action<I> resetor) where I : T
        {
            Type type = typeof(I);
            if (m_PoolDic.TryGetValue(type, out var pool))
            {
                return pool;
            }

            pool = new ObjectPool<T>(() =>
            {
                return creator();
            }, (item) =>
            {
                resetor((I)item);
            });
            return pool;
        }

        public ObjectPool<T> GetPool<I>() where I : T
        {
            if (m_PoolDic.TryGetValue(typeof(I), out var pool))
            {
                return pool;
            }

            return null;
        }

        public void DestroyPool<I>() where I : T
        {
            if (m_PoolDic.TryGetValue(typeof(I), out var pool))
            {
                m_PoolDic.Remove(typeof(I));

                pool.Clear();
            }
        }

        public I Get<I>() where I : T
        {
            var pool = GetPool<I>();
            if (pool != null)
            {
                return (I)pool.Pop();
            }
            return default;
        }

        public void Release<I>(I item) where I : T
        {
            var pool = GetPool<I>();
            if (pool != null)
            {
                pool.Push(item);
            }
        }

    }
}
