using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public class ObjectPool
    {
        private Func<object> m_CreateItemFunc = null;

        public Action<object> RetainAction { get; set; }
        public Action<object> ReleaseAction { get; set; }
        public Action<object> DestroyAction { get; set; }

        private Stack<object> m_ItemStack = null;

        public ObjectPool(Func<object> createFunc)
        {
            m_ItemStack = new Stack<object>();
            m_CreateItemFunc = createFunc;
        }

        public object Get()
        {
            object item = null;

            if (m_ItemStack.Count == 0)
            {
                item = m_CreateItemFunc();
            }
            else
            {
                item = m_ItemStack.Pop();
            }

            RetainAction?.Invoke(item);

            return item;
        }

        public T Get<T>() where T : class
        {
            var item = Get();
            return (T)item;
        }

        public void Release(object item)
        {
            if (m_ItemStack.Contains(item))
            {
                throw new Exception("The item has been realsed.item = " + item);
            }

            ReleaseAction?.Invoke(item);
            m_ItemStack.Push(item);
        }

        public void Clear()
        {
            if (DestroyAction != null)
            {
                foreach (var item in m_ItemStack)
                {
                    DestroyAction(item);
                }
            }
            m_ItemStack.Clear();
        }
    }
}
