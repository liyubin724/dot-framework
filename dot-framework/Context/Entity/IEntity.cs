namespace Dot.Framework
{
    public delegate void EntityControllerChanged(IEntity entity, string name,IController controller);

    public delegate void EntityEvent(IEntity entity);

    public interface IEntity : IAERC, IReusable
    {
        event EntityControllerChanged OnControllerAdded;
        event EntityControllerChanged OnControllerRemoved;

        int Id { get; }
        bool IsEnable { get; }

        void Initialize();
        void Destroy();

        string[] ControllerNames { get; }
        int ControllerCount { get; }

        bool HasController(string name);
        bool HasControllers(string[] names);
        bool HasAnyController(string[] names);

        IController GetController(string name);
        IController[] GetControllers(string[] names);
        IController GetAnyController(string[] names);
        IController[] GetAllControllers();

        void AddController(string name, IController controller);
        void AddControllers(string[] names, IController controllers);

        void RemoveController(string name);
        void RemoveControllers(string[] names);
        void RemoveAllController();

    }
}
