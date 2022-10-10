namespace Dot.Framework
{
    public interface INotifier
    {
        void SendNotification(string notificationName, object body = null, string flag = null);
    }
}