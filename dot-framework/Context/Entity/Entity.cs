using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dot.Framework
{
    public class Entity : IEntity
    {
        public int Id => throw new NotImplementedException();

        public IEntity[] Childs => throw new NotImplementedException();

        public int RetainCount => throw new NotImplementedException();

        public bool IsEnable => throw new NotImplementedException();

        public string[] ControllerNames => throw new NotImplementedException();

        public int ControllerCount => throw new NotImplementedException();

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

        public IController[] GetAllControllers()
        {
            throw new NotImplementedException();
        }

        public IController GetAnyController(string name)
        {
            throw new NotImplementedException();
        }

        public IController GetController(string name)
        {
            throw new NotImplementedException();
        }

        public IController[] GetControllers(string[] names)
        {
            throw new NotImplementedException();
        }

        public bool HasAnyController(string[] names)
        {
            throw new NotImplementedException();
        }

        public bool HasController(string name)
        {
            throw new NotImplementedException();
        }

        public bool HasControllers(string[] names)
        {
            throw new NotImplementedException();
        }

        public void Initilized()
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
