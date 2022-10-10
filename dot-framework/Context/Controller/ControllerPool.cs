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
    }
}
