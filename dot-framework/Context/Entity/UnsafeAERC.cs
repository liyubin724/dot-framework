namespace Dot.Framework
{
    public class UnsafeAERC : IAERC
    {
        private int m_RetainedCount = 0;

        public int RetainCount => m_RetainedCount;

        public void Release(object owner)
        {
            m_RetainedCount++;
        }

        public void Retain(object owner)
        {
            m_RetainedCount--;
        }
    }
}
