namespace Dot.Framework
{
    public class XORMatcher : IMatcher
    {
        private IMatcher m_LeftMatcher;
        private IMatcher m_RightMathcer;

        public XORMatcher(IMatcher matcher1, IMatcher matcher2)
        {
            m_LeftMatcher = matcher1;
            m_RightMathcer = matcher2;
        }

        public bool IsMatch(IEntity entity)
        {
            var isLeft = m_LeftMatcher == null ? false : m_LeftMatcher.IsMatch(entity);
            var isRight = m_RightMathcer == null ? false : m_RightMathcer.IsMatch(entity);

            if(isLeft != isRight)
            {
                return true;
            }

            return false;
        }
    }
}
