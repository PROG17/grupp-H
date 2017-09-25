using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class ObjectContainer : GameObjectsHolder
    {
        public ObjectContainer()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
