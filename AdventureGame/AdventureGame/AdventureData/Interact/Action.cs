using System;
using System.CodeDom.Compiler;
using System.Dynamic;

namespace AdventureGame.AdventureData.Interact
{
    public static class Actions
    {
        public static void Get(Player player, GameObject obj)
        {
            player.Objects.Add(obj.Name, obj as Object);
        }

        public static void Drop(Player player, Object obj, Room room)
        {
            room.Objects.Add(obj.Name, obj);
            player.Objects.Remove(obj.Name);
        }

        public static void Use(Player player, GameObject obj1, GameObject obj2)
        {
            //if (obj1.CanUseWith == obj2.Name)
            //{
            //    obj1 = obj1.ObjectTransformed;
            //}
            if (obj2 is Exit)
            {
                if ((obj1 as Object).CanUseWith == obj2.Name)
                {
                    var door = obj2 as Exit;
                    door.IsLocked = false;
                }
            }
        }

        public static void Look(GameObject obj)
        {
            Console.WriteLine(obj.Description);
        }

        public static void Go(Player player, Room room, Directions direction)
        {
            var exit = room.Exits.Find(p => p.InDirection == direction);
            if (exit != null)
            {
                exit.GoThrough(player);
            }
        }

    }
}