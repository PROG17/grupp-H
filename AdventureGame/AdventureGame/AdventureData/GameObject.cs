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
        public List<string> CanUseWith { get; set; }
        public virtual GameObject ObjectTransformed { get; set; }
        public bool IsGetable { get; set; } = false;
        public bool DropsItemOnUse { get; set; } = false;
        public bool IsHitable { get; set; } = false;

        public GameObject()
        {
            CanUseWith = new List<string>();
        }

        public bool CanBeUsedWith(string obj)
        {
            return CanUseWith.Contains(obj);
        }

        public string Key
        {
            get
            {
                var split = Name.Split(' ');
                return split[split.Length - 1].ToLower();
            }
        }
    }
}
