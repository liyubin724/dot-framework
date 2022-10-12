using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    internal class ObjectPool
    {
        private Func<object> m_CreateItemFunc = null;
        private Action<object> m_ResetItemAction = null;
        private Action<object> m_DestroyItemAction = null;

        public Action<object> ResetAction
        {
            get
            {
                return m_ResetItemAction;
            }
            set
            {
                m_ResetItemAction = value;
            }
        }

        public Action<object> DestroyAction
        {
            get
            {
                return m_DestroyItemAction;
            }
            set
            {
                m_DestroyItemAction = value;
            }
        }


        private Stack<object> m_ItemStack = null;

        public ObjectPool(Func<object> createFunc)
        {
            m_ItemStack = new Stack<object>();
            m_CreateItemFunc = createFunc;
        }

        public object Get()
        {
            object target = null;

            if (m_ItemStack.Count == 0)
            {
                target = m_CreateItemFunc();
            }
            else
            {
                target = m_ItemStack.Pop();
            }
            return target;
        }

        public T Get<T>() where T : class
        {
            var item = Get();
            return (T)item;
        }

        public void Release(object obj)
        {
            if (m_ItemStack.Contains(obj))
            {
                return;
            }

            m_ResetItemAction?.Invoke(obj);
            m_ItemStack.Push(obj);
        }

        public void Clear()
        {
            m_CreateItemFunc = null;
            m_ResetItemAction = null;

            if (m_DestroyItemAction != null)
            {
                foreach (var item in m_ItemStack)
                {
                    m_DestroyItemAction(item);
                }
            }
            m_DestroyItemAction = null;

            m_ItemStack.Clear();
            m_ItemStack = null;
        }
    }

    //internal class ObjectPool<T> where T : class
    //{
    //    private Func<T> createFunc = null;
    //    private Action<T> resetAction = null;

    //    private Stack<T> objectStack = null;

    //    public ObjectPool(Func<T> createFunc, Action<T> resetAction)
    //    {
    //        objectStack = new Stack<T>();
    //        this.createFunc = createFunc;
    //        this.resetAction = resetAction;
    //    }

    //    public T Get()
    //    {
    //        if (objectStack.Count == 0)
    //        {
    //            return createFunc();
    //        }
    //        else
    //        {
    //            return objectStack.Pop();
    //        }
    //    }

    //    public void Release(T obj)
    //    {
    //        if(objectStack.Contains(obj))
    //        {
    //            return;
    //        }

    //        resetAction?.Invoke(obj);
    //        objectStack.Push(obj);
    //    }

    //    public void Clear()
    //    {
    //        createFunc = null;
    //        resetAction = null;

    //        objectStack.Clear();
    //        objectStack = null;
    //    }
    //}
}
