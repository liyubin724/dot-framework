namespace Dot.Framework
{
    public class HaveAllControllerMatcher : IMatcher
    {
        private string[] m_ControllerNames = null;

        public HaveAllControllerMatcher(string[] names)
        {
            m_ControllerNames = names;
        }
        public bool IsMatch(IEntity entity)
        {
            if(m_ControllerNames == null || m_ControllerNames.Length == 0)
            {
                return false;
            }
            return entity.HasControllers(m_ControllerNames);
        }
    }
}
