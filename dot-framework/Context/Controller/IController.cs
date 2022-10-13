namespace Dot.Framework
{
    public interface IController : INotifier, IObserver
    {
        string Name { get;}
        bool IsEnable { get;}
        IEntity Target { get;}

        void Initialize();
        void Activate(string name, IEntity entity);
        void Deactivate();
        void Destroy();
    }
}
