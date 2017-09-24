using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Room : GameObjectsHolder
    {
        public Dictionary<string, Exit> Exits { get; set; }
        public bool IsEndPoint { get; set; }

        //public Room(string name, string description, bool isEndPoint)
        //{
        //    base.Name = name;
        //    base.Description = description;
        //    IsEndPoint = isEndPoint;
        //}

        public bool TryFindExitFromDirection(Room room, Direction key, out Exit exit)
        {
            exit = null;
            foreach (var e in room.Exits)
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

        public string GetRoomDescriptionWithContent()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(this.Description);

            foreach (var gameObject in this.Objects)
            {
                if (gameObject.Value.DirectionalPosition != null)
                {
                    sb.AppendLine($"Åt {gameObject.Value.DirectionalPosition} ser du {gameObject.Value.Name}");
                }
                else
                {
                    sb.AppendLine($"På marken ser du {gameObject.Value.Name}");
                }
            }

            return sb.ToString();
        }

        public bool TryFindObjectInDirection(Room room, Direction key, out GameObject objects)
        {
            foreach (var obj in room.Objects)
            {
                if (obj.Value is Object)
                {
                    if ((obj.Value as Object).DirectionalPosition == key)
                    {
                        objects = obj.Value;
                        return true;
                    }


                }
                else if (obj.Value is Exit)
                {
                    if ((obj.Value as Exit).DirectionalPosition == key)
                    {
                        objects = obj.Value;
                        return true;
                    }

                }
                else if (obj.Value is ObjectContainer)
                {
                    if ((obj.Value as ObjectContainer).DirectionalPosition == key)
                    {
                        objects = obj.Value;
                        return true;
                    }

                }

            }
            objects = null;
            return false;
        }
    }
}
