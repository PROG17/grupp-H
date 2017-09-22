using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Room : GameObjectsHolder
    {
        public Dictionary<string,Exit> Exits { get; set; }
        public bool IsEndPoint { get; set; }

        //public Room(string name, string description, bool isEndPoint)
        //{
        //    base.Name = name;
        //    base.Description = description;
        //    IsEndPoint = isEndPoint;
        //}

        public bool TryFindExitFromDirection(Room room, Directions key, out Exit exit)
        {
            foreach (var e in room.Exits)
            {
                if (e.Value.InDirection == key)
                {
                    exit = e.Value;
                    return true;
                }
            }
            exit = null;
            return false;
        }
    }
}
