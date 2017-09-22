using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class GameObjectContainer : GameObjectsHolder
    {
        //public new Dictionary<String, GameObjectsHolder> Objects { get; set; }
        //public string CanUseWith { get; set; }
        //public GameObjectContainer ObjectTransformed { get; set; }

        public GameObjectContainer()
        {
            //Objects = new Dictionary<string, GameObjectsHolder>();
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
