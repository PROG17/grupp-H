using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class GameObjectContainer : GameObject
    {
        public Dictionary<String, GameObject> Objects { get; set; } 
        public string CanUseWith { get; set; }
        public GameObjectContainer ObjectTransformed { get; set; }
    }
}
