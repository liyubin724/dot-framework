using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dot.Framework
{
    public interface IEntityCenter
    {
        IEntity FindSingleEntity(int id);
        IEntity CreateSingleEntity(int id, string[] controllerNames, Type[] controllerTypes);
        void DestroySingleEntity(int id);

        IEntity FindEntity(int id);
        IEntity CreateEntity(string[] controllerNames, Type[] controllerTypes);
        void DestroyEntity(int id);

        IGroup FindGroup(string name);
        IGroup CreateGroup(string name, IMatcher matcher);
        void DestroyGroup(string name);
    }
}
