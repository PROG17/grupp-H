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

        public string LookInPocket()
        {
            string returnString = "";
            if (Objects.Count != 0)
            {
                Console.WriteLine($"I din ficka ligger det:");
                foreach (var keyValuePair in Objects)
                {
                    returnString +=("\n" + keyValuePair.Value.Name);
                }
                return returnString;
            }
            else
            {
                return ("Den är tom...");
            }
        }

        //public Player(string name, string description, Room playerLocation)
        //{
        //    base.Name = name;
        //    base.Description = description;
        //    PlayerLocation = playerLocation;
        //}
    }
}
