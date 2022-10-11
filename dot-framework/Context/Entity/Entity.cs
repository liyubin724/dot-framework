using System;
using System.Collections.Generic;
using System.Linq;

namespace Dot.Framework
{
    public class Entity : IEntity
    {
        public int Id
        {
            get; internal set;
        }

        private IAERC m_AERC;
        public int RetainCount => m_AERC.RetainCount;

        private bool m_IsEnable = false;
        public bool IsEnable => m_IsEnable;

        private Dictionary<string, IController> m_ControllerDic = new Dictionary<string, IController>();
        public string[] ControllerNames => m_ControllerDic.Keys.ToArray();
        public int ControllerCount => m_ControllerDic.Count;

        public event EntityControllerChanged OnControllerAdded;
        public event EntityControllerChanged OnControllerRemoved;

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
            if (names == null || names.Length == 0)
            {
                return false;
            }
            foreach (var name in names)
            {
                if (m_ControllerDic.ContainsKey(name))
                {
                    return true;
                }
            }
            return false;
        }

        public IController GetController(string name)
        {
            if (m_ControllerDic.TryGetValue(name, out var controller))
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
            for (int i = 0; i < names.Length; i++)
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
                if (m_ControllerDic.TryGetValue(name, out var controller))
                {
                    return controller;
                }
            }
            return null;
        }

        public IController[] GetAllControllers()
        {
            return m_ControllerDic.Values.ToArray();
        }

        public void AddController<C>(string name, bool isSilent = false) where C : IController, new()
        {
            if (!m_IsEnable)
            {
                throw new Exception();
            }

            if (HasController(name))
            {
                throw new Exception();
            }

            var controller = m_ControllerPool.GetController<C>();
            if (controller == null)
            {
                throw new Exception();
            }
            m_ControllerDic.Add(name, controller);

            if (!isSilent)
            {
                OnControllerAdded?.Invoke(this, name);
            }
        }

        public void AddController(string name, Type controllerType, bool isSilent = false)
        {
            if (!m_IsEnable)
            {
                throw new Exception();
            }

            if (controllerType == null || !typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new Exception();
            }

            if (HasController(name))
            {
                throw new Exception();
            }
            var controller = (IController)m_ControllerPool.Get(controllerType);
            if (controller == null)
            {
                throw new Exception();
            }
            m_ControllerDic.Add(name, controller);

            if (!isSilent)
            {
                OnControllerAdded?.Invoke(this, name);
            }
        }

        public void AddControllers(string[] names, Type[] controllerTypes, bool isSilent = false)
        {
            if (names == null || names.Length == 0 || controllerTypes == null || names.Length != controllerTypes.Length)
            {
                throw new Exception();
            }

            for (int i = 0; i < names.Length; i++)
            {
                AddController(names[i], controllerTypes[i], isSilent);
            }
        }

        public void RemoveController(string name, bool isSilent = false)
        {
            if (!m_IsEnable)
            {
                throw new Exception();
            }

            if (m_ControllerDic.TryGetValue(name, out var controller))
            {
                m_ControllerDic.Remove(name);

                m_ControllerPool.ReleaseController(controller);

                if (!isSilent)
                {
                    OnControllerRemoved?.Invoke(this, name);
                }
            }
        }

        public void RemoveControllers(string[] names, bool isSilent = false)
        {
            if (names == null)
            {
                throw new Exception();
            }

            foreach (var name in names)
            {
                RemoveController(name, isSilent);
            }
        }

        public void RemoveAllController(bool isSilent = false)
        {
            var names = m_ControllerDic.Keys.ToArray();
            if (names != null && names.Length > 0)
            {
                RemoveControllers(names, isSilent);
            }
        }

        public void Initialize()
        {
        }

        public void Retain(object owner)
        {
            m_AERC?.Retain(owner);
        }

        public void Release(object owner)
        {
            m_AERC?.Release(owner);
        }

        public void Destroy()
        {
        }

        public void Reset()
        {

        }
    }
}
