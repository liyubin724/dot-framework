using System;
using System.Collections.Generic;

namespace Dot.Framework
{
    public class EntityCenter : IEntityCenter
    {
        private int m_EnityMaxId = 0;

        private ControllerPool m_ControllerPool;
        private ObjectPool m_EntityPool;
        private ObjectPool m_GroupPool;

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

            m_GroupPool = new ObjectPool(() =>
            {
                var group = new Group();
                return group;
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
            entity.Activate(id);

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

                entity.Deactivate();
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
            entity.Activate(m_EnityMaxId);

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
                entity.Deactivate();
                m_EntityPool.Release(entity);
            }
        }

        public IGroup FindGroup(string name)
        {
            return m_GroupDic[name];
        }

        public IGroup CreateGroup(string name,IMatcher matcher)
        {
            if(m_GroupDic.ContainsKey(name))
            {
                throw new Exception();
            }

            var group = (IGroup)m_GroupPool.Get();
            group.Activate(name,matcher);

            m_GroupDic.Add(name, group);

            return group;
        }

        public void DestroyGroup(string name)
        {
            if(!m_GroupDic.TryGetValue(name,out var group))
            {
                return;
            }
            
            m_GroupDic.Remove(name);

            group.Deactivate();
            m_GroupPool.Release(group);
        }
    }
}
