namespace Dot.Framework
{
    public interface IController : INotifier, IObserver, IReusable
    {
        void Activated();
        void Deactivated();
    }
}
