namespace Dot.Framework
{
    public interface IModel : INotifier
    {
        string Name { get; }

        void OnRegistered();
        void OnRemoved();
    }
}
