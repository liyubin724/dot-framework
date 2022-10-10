namespace Dot.Framework
{
    internal class EntityPool : ObjectSetPool<IEntity>
    {
        public ObjectPool<IEntity> CreatePool<I>() where I : IEntity, new()
        {
            return CreatePool(
                    () =>
                    {
                        return new I();
                    },
                    (entity) =>
                    {
                        entity.Reset();
                    });
        }
    }
}
