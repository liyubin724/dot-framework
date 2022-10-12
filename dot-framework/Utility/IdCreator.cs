namespace Dot.Framework
{
    public class IdCreator
    {
        private int m_MaxId = 0;
        public IdCreator(int startId = 0)
        {
            m_MaxId = startId;
        }

        public int GetNext()
        {
            m_MaxId++;
            return m_MaxId;
        }
    }
}
