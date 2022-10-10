namespace Dot.Framework
{
    public class SimpleCommand : Notifier, ICommand
    {
        public virtual void Execute(string notificationName, object body, string flag)
        {

        }
    }
}
