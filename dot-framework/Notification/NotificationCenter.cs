using System.Collections.Generic;

namespace Dot.Framework
{
    public class NotificationCenter : INotificationCenter
    {
        private Dictionary<string, List<IObserver>> m_ObserverDic = new Dictionary<string, List<IObserver>>();
        private Stack<string> m_NotifyingStack = new Stack<string>();

        private List<IObserver> m_WillAddedObservers = new List<IObserver>();
        private List<IObserver> m_WillRemovedObservers = new List<IObserver>();

        public bool HasNotifier(string name)
        {
            return m_ObserverDic.ContainsKey(name);
        }

        public void NotifyObserver(string name, object body, string flag)
        {
            if (m_NotifyingStack.Contains(name))
            {
                throw new NotifyEndlessLoopException();
            }

            m_NotifyingStack.Push(name);
            {
                if (m_ObserverDic.TryGetValue(name, out var observers))
                {
                    foreach (var observer in observers)
                    {
                        observer.HandleNotification(name, body, flag);
                    }
                }
            }
            m_NotifyingStack.Pop();

            if (m_NotifyingStack.Count == 0)
            {
                if (m_WillAddedObservers.Count > 0)
                {
                    foreach (var observer in m_WillAddedObservers)
                    {
                        RegisterObserver(observer);
                    }

                    m_WillAddedObservers.Clear();
                }

                if (m_WillRemovedObservers.Count > 0)
                {
                    foreach (var observer in m_WillRemovedObservers)
                    {
                        RemoveObserver(observer);
                    }

                    m_WillRemovedObservers.Clear();
                }
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            if (m_NotifyingStack.Count > 0)
            {
                m_WillAddedObservers.Add(observer);
            }
            else
            {
                var names = observer.ListNotificationInterests();
                if (names != null && names.Length > 0)
                {
                    foreach (var name in names)
                    {
                        if (!m_ObserverDic.TryGetValue(name, out var observers))
                        {
                            observers = new List<IObserver>();
                            m_ObserverDic[name] = observers;
                        }
                        
                        if(observers.Contains(observer))
                        {
                            throw new ObserverAlreadyAddedException();
                        }

                        observers.Add(observer);
                    }
                }
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            if (m_NotifyingStack.Count > 0)
            {
                m_WillRemovedObservers.Add(observer);
            }
            else
            {
                var names = observer.ListNotificationInterests();
                if (names != null && names.Length > 0)
                {
                    foreach (var name in names)
                    {
                        if (m_ObserverDic.TryGetValue(name, out var observers))
                        {
                            observers.Remove(observer);
                        }
                    }
                }
            }
        }
    }
}
