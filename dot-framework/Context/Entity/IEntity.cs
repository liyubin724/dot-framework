using System;

namespace Dot.Framework
{
    public delegate void EntityControllerChanged(IEntity entity, string name);

    public delegate void EntityEvent(IEntity entity);

    public interface IEntity : IAERC
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

        void AddController<C>(string name,bool isSilent = false) where C : IController, new();
        void AddController(string name, Type controllerType,bool isSilent = false);
        void AddControllers(string[] names, Type[] controllerTypes, bool isSilent = false);

        void RemoveController(string name, bool isSilent = false);
        void RemoveControllers(string[] names,bool isSilent = false);
        void RemoveAllController(bool isSilent = false);

    }
}
