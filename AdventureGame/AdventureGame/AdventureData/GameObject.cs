using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<Direction> DirectionalPosition { get; set; }
        public string CanUseWith { get; set; }
        public GameObject ObjectTransformed { get; set; }

        public string Key
        {
            get
            {
                var split = Name.Split(' ');
                return split[split.Length - 1];
            }
        }
    }
}
