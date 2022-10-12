namespace Dot.Framework
{
    public class AndMatcher : IMatcher
    {
        private IMatcher m_LeftMatcher;
        private IMatcher m_RightMathcer;

        public AndMatcher(IMatcher matcher1, IMatcher matcher2)
        {
            m_LeftMatcher = matcher1;
            m_RightMathcer = matcher2;
        }

        public bool IsMatch(IEntity entity)
        {
            if (m_LeftMatcher == null || m_RightMathcer == null)
            {
                return false;
            }

            return m_LeftMatcher.IsMatch(entity) && m_RightMathcer.IsMatch(entity);
        }
    }
}
