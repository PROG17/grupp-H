using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Directions Direction { get; set; }
    }
}
