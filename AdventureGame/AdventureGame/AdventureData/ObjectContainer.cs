using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class ObjectContainer : GameObjectsHolder
    {
        // Overridad GetContentAsString
        public override string GetContentAsString()
        {
            string returnString = "Du tittar i den och ser:\n";
            return returnString + base.GetContentAsString();
        }

        public ObjectContainer()
        {
            IsGetable = false;
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
