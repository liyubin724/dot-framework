namespace Dot.Framework
{
    public interface IModelCenter
    {
        IModel this[string name] { get;}

        bool HasProxy(string proxyName);
        void RegisterProxy(IModel proxy);
        IModel RetrieveProxy(string proxyName);
        IModel RemoveProxy(string proxyName);
    }
}
