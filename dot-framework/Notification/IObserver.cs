namespace Dot.Framework
{
    public interface IObserver
    {
        string[] ListNotificationInterests();
        void HandleNotification(string name, object body, string flag);
    }
}
