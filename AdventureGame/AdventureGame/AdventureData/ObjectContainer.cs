using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class ObjectContainer : GameObjectsHolder
    {
        //public new Dictionary<String, GameObjectsHolder> Objects { get; set; }
        //public string CanUseWith { get; set; }
        //public ObjectContainer ObjectTransformed { get; set; }
        private bool isPeekable;

        public ObjectContainer()
        {
            //Objects = new Dictionary<string, GameObjectsHolder>();
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
