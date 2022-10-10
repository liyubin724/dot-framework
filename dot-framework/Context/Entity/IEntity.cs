namespace Dot.Framework
{
    public interface IEntity : IAERC
    {
        int Id { get; }
        bool IsEnable { get; }
        IEntity[] Childs { get; }

        void Initilized();
        void Destroy();

        void Attach(IEntity child);
        void Detach(IEntity child);

        void AttachToParent(IEntity parent);
        void DetachFromParent();

        string[] ControllerNames { get; }
        int ControllerCount { get; }

        bool HasController(string name);
        bool HasControllers(string[] names);
        bool HasAnyController(string[] names);

        IController GetController(string name);
        IController[] GetControllers(string[] names);
        IController GetAnyController(string name);
        IController[] GetAllControllers();

        void AddController(string name, IController controller);
        void AddControllers(string[] names, IController controllers);

        void RemoveController(string name);
        void RemoveControllers(string[] names);
        void RemoveAllController();

    }
}
