namespace Dot.Framework
{
    public interface IController : INotifier, IObserver
    {
        void Activated();
        void Deactivated();
    }
}
