using System;

namespace Dot.Framework
{
    public interface ICommandCenter
    {
        void RegisterCommand(string name, Func<ICommand> factory);
        void RemoveCommand(string name);
        bool HasCommand(string name);
        bool ExcuteCommand(string name, object body, string flag);
    }
}
