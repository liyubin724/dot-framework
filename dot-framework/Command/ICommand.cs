namespace Dot.Framework
{
    public interface ICommand : INotifier
    {
        void Execute(string name, object body, string flag);
    }
}