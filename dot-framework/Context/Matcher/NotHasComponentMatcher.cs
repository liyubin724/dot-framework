namespace Dot.Framework
{
    public class NotHasComponentMatcher : IMatcher
    {
        private string m_ControllerName = null;
        public NotHasComponentMatcher(string name)
        {
            m_ControllerName = name;
        }

        public bool IsMatch(IEntity entity)
        {
            if (string.IsNullOrEmpty(m_ControllerName))
            {
                return false;
            }

            return !entity.HasController(m_ControllerName);
        }
    }
}
