using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public class MacroCommand : Notifier, ICommand
    {
        private IList<Func<ICommand>> m_SubcommandFactories;

        public MacroCommand()
        {
            m_SubcommandFactories = new List<Func<ICommand>>();
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {

        }

        protected void AddSubCommand(Func<ICommand> factory)
        {
            m_SubcommandFactories.Add(factory);
        }

        public void Execute(string notificationName, object body, string flag)
        {
            while (m_SubcommandFactories.Count > 0)
            {
                var factory = m_SubcommandFactories[0];
                var command = factory();
                command.Execute(notificationName, body, flag);
                m_SubcommandFactories.RemoveAt(0);
            }
        }
    }
}
