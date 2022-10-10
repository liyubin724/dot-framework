using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dot.Framework
{
    public class Entity : IEntity
    {
        private int m_Id = 0;
        public int Id => m_Id;

        private IAERC m_AERC;
        public int RetainCount => m_AERC.RetainCount;

        private bool m_IsEnable = false;
        public bool IsEnable => m_IsEnable;

        private Dictionary<string, IController> m_ControllerDic = new Dictionary<string, IController>();
        public string[] ControllerNames => m_ControllerDic.Keys.ToArray();
        public int ControllerCount => m_ControllerDic.Count;

        private ControllerPool m_ControllerPool = null;

        public bool HasController(string name)
        {
            return m_ControllerDic.ContainsKey(name);
        }

        public bool HasControllers(string[] names)
        {
            if (names == null || names.Length == 0)
            {
                return false;
            }

            foreach (var name in names)
            {
                if (!m_ControllerDic.ContainsKey(name))
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasAnyController(string[] names)
        {
            if(names == null || names.Length == 0)
            {
                return false;
            }
            foreach(var name in names)
            {
                if(m_ControllerDic.ContainsKey(name))
                {
                    return true;
                }
            }
            return false;
        }

        public IController GetController(string name)
        {
            if(m_ControllerDic.TryGetValue(name,out var controller))
            {
                return controller;
            }
            return null;
        }

        public IController[] GetControllers(string[] names)
        {
            if (names == null || names.Length == 0)
            {
                return null;
            }

            IController[] controllers = new IController[names.Length];
            for(int i =0;i<names.Length;i++)
            {
                controllers[i] = GetController(names[i]);
            }
            return controllers;
        }

       

        public IController GetAnyController(string[] names)
        {
            if (names == null || names.Length == 0)
            {
                return null;
            }
            foreach (var name in names)
            {
                if (m_ControllerDic.TryGetValue(name,out var controller))
                {
                    return controller;
                }
            }
            return null ;
        }

        public IController[] GetAllControllers()
        {
            return m_ControllerDic.Values.ToArray();
        }

        public void AddController<T>(string name) where T:IController,new()
        {
            var controller = m_ControllerPool.Get<T>();

        }



        public void AddController(string name, IController controller)
        {
            throw new NotImplementedException();
        }

        public void AddControllers(string[] names, IController controllers)
        {
            throw new NotImplementedException();
        }

        public void Attach(IEntity child)
        {
            throw new NotImplementedException();
        }

        public void AttachToParent(IEntity parent)
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void Detach(IEntity child)
        {
            throw new NotImplementedException();
        }

        public void DetachFromParent()
        {
            throw new NotImplementedException();
        }

        public void Release(object owner)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllController()
        {
            throw new NotImplementedException();
        }

        public void RemoveController(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveControllers(string[] names)
        {
            throw new NotImplementedException();
        }

        public void Retain(object owner)
        {
            throw new NotImplementedException();
        }
    }
}
