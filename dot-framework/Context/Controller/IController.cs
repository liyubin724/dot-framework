namespace Dot.Framework
{
    public interface IController : INotifier, IObserver
    {
        string Name { get;}
        bool IsEnable { get;}
        IEntity Target { get;}

        void Initialize();
        void Activated(string name,IEntity entity);
        void Deactivated();
        void Destroy();
    }
}
