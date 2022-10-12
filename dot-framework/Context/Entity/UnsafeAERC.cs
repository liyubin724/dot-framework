namespace Dot.Framework
{
    public class UnsafeAERC : IAERC
    {
        private int m_RetainedCount = 0;

        public int RefCount => m_RetainedCount;

        public void ReleaseRef(object owner)
        {
            m_RetainedCount++;
        }

        public void RetainRef(object owner)
        {
            m_RetainedCount--;
        }
    }
}
