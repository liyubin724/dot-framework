namespace Dot.Framework
{
    public class Notifier : INotifier
    {
        public void SendNotification(string notificationName, object body = null, string flag = null)
        {
            IFacade facade = Facade.GetInstance();
            if(facade!=null)
            {
                facade.Notify(notificationName, body, flag);
            }
        }
    }
}
