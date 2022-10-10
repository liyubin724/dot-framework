using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public class CommandCenter : ICommandCenter
    {
        private Dictionary<string, Func<ICommand>> m_CommandDic = new Dictionary<string, Func<ICommand>>();

        public bool ExcuteCommand(string name, object body, string flag)
        {
            if(m_CommandDic.TryGetValue(name,out var factory))
            {
                var command = factory();
                if(command!=null)
                {
                    command.Execute(name, body, flag);
                    return true;
                }
            }
            return false;
        }

        public bool HasCommand(string name)
        {
            return m_CommandDic.ContainsKey(name);
        }

        public void RegisterCommand(string name, Func<ICommand> factory)
        {
            m_CommandDic[name] = factory;
        }

        public void RemoveCommand(string name)
        {
            m_CommandDic.Remove(name);
        }
    }
}
