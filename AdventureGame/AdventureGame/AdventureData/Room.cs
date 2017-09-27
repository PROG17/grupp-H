using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Room : GameObjectsHolder
    {
        // Lista med utgångar för varje rum
        public Dictionary<string, Exit> Exits { get; set; }

        // Bool för att kolla om spelaren är i slutrummet
        public bool IsEndPoint { get; set; }

        // Konstruktor
        public Room()
        {
            Exits = new Dictionary<string, Exit>();
        }

        // Metod för att fösröka hitta en utgång i 
        public bool TryFindExitFromDirection(Direction key, out Exit exit)
        {
            exit = null;
            foreach (var e in this.Exits)
            {
                if (e.Value.DirectionalPosition == key)
                {
                    exit = e.Value;
                    return true;
                }
            }
            exit = null;
            return false;
        }

        public override string GetContentAsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(this.Description);

            List<string> groundItems = new List<string>();


            foreach (var gameObject in this.Objects)
            {
                if (gameObject.Value.DirectionalPosition != null)
                {
                    sb.AppendLine($"Åt {gameObject.Value.DirectionalPosition} ser du {gameObject.Value.Name}");
                }
                else
                {
                    groundItems.Add($"På marken ser du {gameObject.Value.Name}");
                }
            }
            foreach (var groundItem in groundItems)
            {
                sb.AppendLine(groundItem);
            }

            return sb.ToString();
        }

        public bool TryFindObjectInDirection(Direction key, out GameObject objects)
        {
            foreach (var obj in this.Objects)
            {
                if (obj.Value.DirectionalPosition == key)
                {
                    objects = obj.Value;
                    return true;
                }
            }
            objects = null;
            return false;
        }
    }
}
