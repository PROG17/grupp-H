using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Exit : GameObject
    {
        public Room GoesTo { get; set; }
        public Object OpensWith { get; set; }
        public Directions InDirection { get; set; }
        public bool IsLocked { get; set; }

        //public Exit(string name, string description, Room goesTo, Object opensWith, Directions inDirection, bool isLocked)
        //{
        //    base.Name = name;
        //    base.Description = description;
        //    GoesTo = goesTo;
        //    OpensWith = opensWith;   
        //    InDirection = inDirection;
        //    IsLocked = isLocked;
        //}

        public void GoThrough(Player player)
        {
            player.PlayerLocation = GoesTo;
        }
    }
}
