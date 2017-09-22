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

        //public Player(string name, string description, Room playerLocation)
        //{
        //    base.Name = name;
        //    base.Description = description;
        //    PlayerLocation = playerLocation;
        //}
    }
}
