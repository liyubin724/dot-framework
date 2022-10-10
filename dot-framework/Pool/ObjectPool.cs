using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    internal class ObjectPool
    {
        private Func<object> createFunc = null;
        private Action<object> resetAction = null;

        private Stack<object> objectStack = null;

        public ObjectPool(Func<object> create, Action<object> release)
        {
            objectStack = new Stack<object>();
            createFunc = create;
            resetAction = release;
        }

        public object Pop()
        {
            object target = null;

            if (objectStack.Count == 0)
            {
                target = createFunc();
            }
            else
            {
                target = objectStack.Pop();
            }
            return target;
        }

        public void Push(object obj)
        {
            if(objectStack.Contains(obj))
            {
                return;
            }

            resetAction?.Invoke(obj);
            objectStack.Push(obj);
        }

        public void Clear()
        {
            createFunc = null;
            resetAction = null;

            objectStack.Clear();
            objectStack = null;
        }
    }

    internal class ObjectPool<T> where T : class
    {
        private Func<T> createFunc = null;
        private Action<T> resetAction = null;

        private Stack<T> objectStack = null;

        public ObjectPool(Func<T> creater, Action<T> reseter)
        {
            objectStack = new Stack<T>();
            createFunc = creater;
            resetAction = reseter;
        }

        public T Pop()
        {
            if (objectStack.Count == 0)
            {
                return createFunc();
            }
            else
            {
                return objectStack.Pop();
            }
        }

        public void Push(T obj)
        {
            if(objectStack.Contains(obj))
            {
                return;
            }

            resetAction?.Invoke(obj);
            objectStack.Push(obj);
        }

        public void Clear()
        {
            createFunc = null;
            resetAction = null;

            objectStack.Clear();
            objectStack = null;
        }
    }
}
