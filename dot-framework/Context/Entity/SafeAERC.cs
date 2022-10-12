using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public sealed class SafeAERC : IAERC
    {
        public int RefCount => m_Owners.Count;

        private IEntity m_Enity;
        private HashSet<object> m_Owners = new HashSet<object>();
        public SafeAERC(IEntity entity)
        {
            m_Enity = entity;
        }

        public void ReleaseRef(object owner)
        {
            if(!m_Owners.Add(owner))
            {
                throw new EntityIsAlreadyRetainedByOwnerException(m_Enity, owner);
            }
        }

        public void RetainRef(object owner)
        {
            if(!m_Owners.Remove(owner))
            {
                throw new EntityIsNotRetainedByOwnerException(m_Enity, owner);
            }
        }
    }
}
