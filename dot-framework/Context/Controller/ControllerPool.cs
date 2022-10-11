namespace Dot.Framework
{
    internal class ControllerPool : ObjectSetPool<IController>
    {
        public ObjectPool<IController> CreatePool<I>() where I : IController, new()
        {
            return CreatePool(
                    () =>
                    {
                        return new I();
                    },
                    (controller) =>
                    {
                        controller.Reset();
                    }
                );
        }

        public I GetController<I>(bool createIfNot = true) where I:IController, new()
        {
            if(!HasPool<I>() && createIfNot)
            {
                CreatePool<I>();
            }

            return Get<I>();
        }

        public void ReleaseController<I>(I item,bool createIfNot = true) where I:IController,new()
        {
            if (!HasPool<I>() && createIfNot)
            {
                CreatePool<I>();
            }

            Release(item);
        }
    }
}
