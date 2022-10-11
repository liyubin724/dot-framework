using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    internal class ObjectPool
    {
        private Func<object> createFunc = null;
        private Action<object> resetAction = null;

        private Stack<object> objectStack = null;

        public ObjectPool(Func<object> createFunc, Action<object> resetAction)
        {
            objectStack = new Stack<object>();
            this.createFunc = createFunc;
            this.resetAction = resetAction;
        }

        public object Get()
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

        public void Release(object obj)
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

        public ObjectPool(Func<T> createFunc, Action<T> resetAction)
        {
            objectStack = new Stack<T>();
            this.createFunc = createFunc;
            this.resetAction = resetAction;
        }

        public T Get()
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

        public void Release(T obj)
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
