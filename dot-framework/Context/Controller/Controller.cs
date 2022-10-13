namespace Dot.Framework
{
    public class Controller : Notifier, IController
    {
        public string Name { get; private set; }
        public bool IsEnable { get; private set; }

        public IEntity Target { get; private set; }

        public void Initialize()
        {
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        public void Activate(string name, IEntity entity)
        {
            if(IsEnable)
            {
                return;
            }
            IsEnable = true;
            OnActivated();
        }

        protected virtual void OnActivated()
        {
        }

        public void Deactivate()
        {
            if(!IsEnable)
            {
                return;
            }

            OnDeactivated();

            Name = null;
            IsEnable = false;
            Target = null;
        }

        protected virtual void OnDeactivated()
        {
        }

        public void Destroy()
        {
            OnDestroy();

            Name = null;
            IsEnable = false;
            Target = null;
        }

        protected virtual void OnDestroy()
        {
        }

        public virtual string[] ListNotificationInterests()
        {
            return null;
        }

        public void HandleNotification(string name, object body, string flag)
        {
            if (!IsEnable)
            {
                return;
            }

            OnHandleNotification(name, body, flag);
        }

        protected virtual void OnHandleNotification(string name, object body, string flag)
        {

        }
    }
}
