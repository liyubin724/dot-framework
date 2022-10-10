using System.Collections.Generic;

namespace Dot.Framework
{
    public class ModelCenter : IModelCenter
    {
        private Dictionary<string, IModel> m_ProxyDic = new Dictionary<string, IModel>();

        public IModel this[string name]
        {
            get
            {
                return m_ProxyDic[name];
            }
        }

        public bool HasProxy(string proxyName)
        {
            return m_ProxyDic.ContainsKey(proxyName);
        }

        public void RegisterProxy(IModel proxy)
        {
            m_ProxyDic[proxy.Name] = proxy;
            proxy.OnRegistered();
        }

        public IModel RemoveProxy(string proxyName)
        {
            if (m_ProxyDic.TryGetValue(proxyName, out var proxy))
            {
                proxy.OnRemoved();
            }
            return proxy;
        }

        public IModel RetrieveProxy(string proxyName)
        {
            return m_ProxyDic.TryGetValue(proxyName, out var proxy) ? proxy : null;
        }
    }
}
