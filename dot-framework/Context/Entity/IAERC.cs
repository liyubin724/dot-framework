namespace Dot.Framework
{
    public interface IAERC
    {
        int RefCount { get; }
        void RetainRef(object owner);
        void ReleaseRef(object owner);
    }
}
