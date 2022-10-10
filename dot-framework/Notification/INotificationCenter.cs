namespace Dot.Framework
{
    public interface INotificationCenter
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);

        bool HasNotifier(string name);

        void NotifyObserver(string name, object body, string flag);
    }
}
