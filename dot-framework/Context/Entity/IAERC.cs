namespace Dot.Framework
{
    public interface IAERC
    {
        int RetainCount { get; }
        void Retain(object owner);
        void Release(object owner);
    }
}
