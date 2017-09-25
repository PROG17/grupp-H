using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Player : GameObjectsHolder
    {
        public Room PlayerLocation { get; set; }

        public bool IsAlive { get; internal set; }

        public override string GetContentAsString()
        {
            if (Objects.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("I din ficka ligger det:\n");
                sb.Append(base.GetContentAsString());
                return sb.ToString();
            }
            else
            {
                return ("Den är tom...");
            }
        }

        public void IsFighting(bool isKilled)
        {
            if (isKilled)
            {
                IsAlive = false;
            }
        }
    }
}
