using System;

namespace Dot.Framework
{
    public interface IFacade
    {
        IModelCenter ModelCenter { get; }
        ICommandCenter CommandCenter { get; }
        IEntityCenter EntityContext { get; }

        void Initialize();
        void Destroy();

        bool HasProxy(string name);
        void RegisterProxy(IModel proxy);
        IModel RetrieveProxy(string name);
        IModel RemoveProxy(string name);

        void RegisterCommand(string name, Func<ICommand> factory);
        void RemoveCommand(string name);
        bool HasCommand(string name);

        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);

        void Notify(string name, object body, string flag);
    }
}
