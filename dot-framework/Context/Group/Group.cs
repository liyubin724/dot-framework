using System.Collections.Generic;
using System.Linq;

namespace Dot.Framework
{
    public class Group : IGroup
    {
        public string Name { get; private set; }
        public IMatcher Matcher { get;private set; }

        private Dictionary<int, IEntity> m_EntityDic = new Dictionary<int, IEntity>();
        private IEntity[] m_CachedEntities = null;
        public IEntity[] Entities
        {
            get
            {
                if (m_CachedEntities == null)
                {
                    m_CachedEntities = m_EntityDic.Values.ToArray();
                }
                return m_CachedEntities;
            }
        }

        private int m_RefCount = 0;
        public int RefCount => m_RefCount;

        public event GroupChanged OnEntityAdded;
        public event GroupChanged OnEntityRemoved;

        public void ReleaseRef()
        {
            m_RefCount++;
        }

        public void RetainRef()
        {
            m_RefCount--;
        }

        public void Activate(string name,IMatcher matcher)
        {
            Name = name;
            Matcher = matcher;
        }

        public void Deactivate()
        {
            Name = null;
            Matcher = null;
        }

        public bool TryUpdate(IEntity entity, string controllerName)
        {
            if (Matcher == null)
            {
                return false;
            }
            if (Matcher.IsMatch(entity))
            {
                if (AddEntitySilent(entity))
                {
                    OnEntityAdded?.Invoke(this, entity);
                    return true;
                }
            }
            else
            {
                if (RemoveEntitySilent(entity))
                {
                    OnEntityRemoved?.Invoke(this, entity);
                    return true;
                }
            }
            return false;
        }


        private bool AddEntitySilent(IEntity entity)
        {
            if (!m_EntityDic.ContainsKey(entity.Id))
            {
                m_EntityDic.Add(entity.Id, entity);

                m_CachedEntities = null;

                return true;
            }
            return false;
        }

        private bool RemoveEntitySilent(IEntity entity)
        {
            if (m_EntityDic.ContainsKey(entity.Id))
            {
                m_EntityDic.Remove(entity.Id);

                m_CachedEntities = null;

                return true;
            }
            return false;
        }
    }
}
