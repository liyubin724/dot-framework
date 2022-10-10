namespace Dot.Framework
{
    public class Controller : Notifier, IController
    {
        public virtual void Activated()
        {
        }

        public virtual void Deactivated()
        {
        }

        public virtual void HandleNotification(string name, object body, string flag)
        {
        }

        public virtual string[] ListNotificationInterests()
        {
            return null;
        }

        public virtual void Reset()
        {
        }
    }
}
