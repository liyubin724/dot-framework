namespace Dot.Framework
{
    public class OrMatcher
    {
        private IMatcher m_LeftMatcher;
        private IMatcher m_RightMathcer;

        public OrMatcher(IMatcher matcher1, IMatcher matcher2)
        {
            m_LeftMatcher = matcher1;
            m_RightMathcer = matcher2;
        }

        public bool IsMatch(IEntity entity)
        {
            return m_LeftMatcher == null ? false : m_LeftMatcher.IsMatch(entity) ||
                m_RightMathcer == null ? false : m_RightMathcer.IsMatch(entity);
        }
    }
}
