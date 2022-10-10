using System;

namespace Dot.Framework
{
    public class Facade : IFacade
    {
        protected static IFacade instance;

        public static IFacade GetInstance()
        {
            return instance;
        }

        public static IFacade CreateInstance(Func<IFacade> facadeCreator)
        {
            if(instance !=null)
            {
                return instance;
            }
            if(facadeCreator!=null)
            {
                instance = facadeCreator();
            }
            else
            {
                instance = new Facade();
            }
            instance.Initialize();
            return instance;
        }

        public static void DestroyInstance()
        {
            if(instance!=null)
            {
                instance.Destroy();
                instance = null;
            }
        }

        protected IModelCenter modelCenter;
        protected ICommandCenter commandCenter;
        protected IEntityCenter enityContext;
        protected INotificationCenter notificationCenter;

        public IModelCenter ModelCenter => modelCenter;
        public ICommandCenter CommandCenter => commandCenter;
        public IEntityCenter EntityContext => enityContext;

        public void Initialize()
        {
            InitializeModel();
            InitializeCommand();
            InitializeNotification();
            InitializeEntity();
        }

        protected virtual void InitializeModel()
        {
            modelCenter = new ModelCenter();
        }

        protected virtual void InitializeCommand()
        {
            commandCenter = new CommandCenter();
        }

        protected virtual void InitializeNotification()
        {
            notificationCenter = new NotificationCenter();
        }

        protected virtual void InitializeEntity()
        {
            
        }

        public void Destroy()
        {

        }

        public bool HasProxy(string name)
        {
            return modelCenter.HasProxy(name);
        }

        public void RegisterProxy(IModel proxy)
        {
            modelCenter.RegisterProxy(proxy);
        }

        public IModel RetrieveProxy(string name)
        {
            return modelCenter.RetrieveProxy(name);
        }

        public IModel RemoveProxy(string name)
        {
            return modelCenter.RemoveProxy(name);
        }

        public bool HasCommand(string name)
        {
            return commandCenter.HasCommand(name);
        }

        public void RegisterCommand(string name,Func<ICommand> factory)
        {
            commandCenter.RegisterCommand(name, factory);
        }

        public void RemoveCommand(string name)
        {
            commandCenter.RemoveCommand(name);
        }

        public void RegisterObserver(IObserver observer)
        {
            notificationCenter.RegisterObserver(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            notificationCenter.RemoveObserver(observer);
        }

        public void Notify(string name,object body,string flag)
        {
            if(commandCenter.HasCommand(name))
            {
                commandCenter.ExcuteCommand(name, body, flag);
            }

            if(notificationCenter.HasNotifier(name))
            {
                notificationCenter.NotifyObserver(name, body, flag);
            }
        }
    }
}
