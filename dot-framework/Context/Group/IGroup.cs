namespace Dot.Framework
{
    public enum EGroupEvent
    {
        Added = 0,
        Removed,
        AddedOrRemoved,
    }

    public delegate void GroupChanged(IGroup group, IEntity entity);

    public interface IGroup
    {
        IMatcher Matcher { get; }
        IEntity[] Entities { get; }

        event GroupChanged OnEntityAdded;
        event GroupChanged OnEntityRemoved;

        int RefCount { get; }
        void RetainRef();
        void ReleaseRef();

        bool TryUpdate(IEntity entity, string controllerName);
    }
}
