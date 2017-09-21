using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class GameObjectsHolder : GameObject
    {
        public List<Object> Objects { get; set; }

        public GameObjectsHolder()
        {
            Objects = new List<Object>();
        }
    }
}
