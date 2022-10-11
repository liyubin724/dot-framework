namespace Dot.Framework.Context.Matcher
{
    public class NotMatcher : IMatcher
    {
        private IMatcher m_Mathcer;

        public NotMatcher(IMatcher mathcer)
        {
            m_Mathcer = mathcer;
        }

        public bool IsMatch(IEntity entity)
        {
            if(m_Mathcer == null)
            {
                return false;
            }
            return m_Mathcer.IsMatch(entity);
        }
    }
}
