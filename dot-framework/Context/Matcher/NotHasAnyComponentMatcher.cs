namespace Dot.Framework
{
    public class NotHasAnyComponentMatcher : IMatcher
    {
        private string[] m_ControllerNames = null;

        public NotHasAnyComponentMatcher(string[] names)
        {
            m_ControllerNames = names;
        }
        public bool IsMatch(IEntity entity)
        {
            if (m_ControllerNames == null || m_ControllerNames.Length == 0)
            {
                return false;
            }
            return !entity.HasAnyController(m_ControllerNames);
        }
    }
}
