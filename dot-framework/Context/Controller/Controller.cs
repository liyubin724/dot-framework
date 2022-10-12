namespace Dot.Framework
{
    public class Controller : Notifier, IController
    {
        public string Name { get; private set; }
        public bool IsEnable { get; private set; }

        public IEntity Target { get; private set; }

        public virtual void Initialize()
        {
        }

        public void Activated(string name, IEntity target)
        {
            Name = name;
            IsEnable = true;
            Target = target;
        }

        public virtual void Deactivated()
        {
            Name = null;
            IsEnable = false;
            Target = null;
        }

        public virtual void Destroy()
        {
        }

        public virtual string[] ListNotificationInterests()
        {
            return null;
        }

        public virtual void HandleNotification(string name, object body, string flag)
        {
        }
    }
}
