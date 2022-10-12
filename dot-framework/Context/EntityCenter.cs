using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dot.Framework
{
    public class EntityCenter : IEntityCenter
    {
        private int m_EnityMaxId = 0;

        private ControllerPool m_ControllerPool;
        private ObjectPool m_EntityPool;

        private Dictionary<int, IEntity> m_EntityDic = new Dictionary<int, IEntity>();
        private Dictionary<int, IEntity> m_SingleEntityDic = new Dictionary<int, IEntity>();
        private Dictionary<string, IGroup> m_GroupDic = new Dictionary<string, IGroup>();

        public EntityCenter()
        {
            m_ControllerPool = new ControllerPool();
            m_EntityPool = new ObjectPool(() =>
            {
                var entity = new Entity();
                entity.Initialize(m_ControllerPool);
                return entity;
            });
        }

        public IEntity FindSingleEntity(int id)
        {
            return m_SingleEntityDic[id];
        }

        public IEntity CreateSingleEntity(int id, string[] controllerNames,Type[] controllerTypes)
        {
            if(m_SingleEntityDic.TryGetValue(id,out var entity))
            {
                return entity;
            }

            entity = m_EntityPool.Get() as IEntity;
            entity.GetFromPool(id);

            if(controllerNames!=null)
            {
                entity.AddControllers(controllerNames, controllerTypes, true);
            }

            m_SingleEntityDic.Add(id, entity);

            return entity;
        }

        public void DestroySingleEntity(int id)
        {
            if (m_SingleEntityDic.TryGetValue(id, out var entity))
            {
                m_SingleEntityDic.Remove(id);

                entity.ReleaseToPool();
                m_EntityPool.Release(entity);
            }
        }

        public IEntity FindEntity(int id)
        {
            return m_EntityDic[id];
        }

        public IEntity CreateEntity(string[] controllerNames, Type[] controllerTypes)
        {
            m_EnityMaxId++;

            var entity = m_EntityPool.Get() as IEntity;
            entity.GetFromPool(m_EnityMaxId);

            if(controllerNames != null)
            {
                entity.AddControllers(controllerNames, controllerTypes);
            }

            m_EntityDic.Add(entity.Id, entity);
            return entity;
        }

        public void DestroyEntity(int id)
        {
            if(m_EntityDic.TryGetValue(id,out var entity))
            {
                m_EntityDic.Remove(id);

                entity.RemoveAllController();
                entity.ReleaseToPool();
                m_EntityPool.Release(entity);
            }
        }

        public IGroup CreateGroup(string name,IMatcher matcher)
        {
            if(m_GroupDic.TryGetValue(name,out var group))
            {
                if(group.Matcher == matcher)
                {
                    return group;
                }
                else
                {
                    throw new Exception();
                }
            }

            return null;
        }

        public void DestroyGroup(string name)
        {

        }
    }
}
