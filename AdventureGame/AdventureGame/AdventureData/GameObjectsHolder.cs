using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class GameObjectsHolder : GameObject
    {
        public virtual Dictionary<string, GameObject> Objects { get; set; }

        public GameObjectsHolder()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
