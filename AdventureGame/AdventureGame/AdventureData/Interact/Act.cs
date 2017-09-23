using System;
using System.CodeDom.Compiler;
using System.Dynamic;
using System.Xml.Linq;

namespace AdventureGame.AdventureData.Interact
{
    public static class Act
    {
        public static void Get(Player player, GameObject obj)
        {
            player.Objects.Add(obj.Name, obj as Object);
            player.PlayerLocation.Objects.Remove(obj.Name);
        }

        public static void Drop(Player player, Object obj, Room room)
        {
            room.Objects.Add(obj.Name, obj);
            player.Objects.Remove(obj.Name);
        }

        public static bool Use(Player player, GameObject obj1, GameObject obj2)
        {
            //if (obj1.CanUseWith == obj2.Name)
            //{
            //    obj1 = obj1.ObjectTransformed;
            //}
            //if (obj2 is Exit)
            //{
            //    if ((obj1 as Object).CanUseWith == obj2.Name)
            //    {
            //        var door = obj2 as Exit;
            //        door.IsLocked = false;
            //    }
            //}

            if (obj1.CanUseWith == obj2.Name)
            {
                player.PlayerLocation.Objects.Remove(obj2.Name);
                player.PlayerLocation.Objects.Add(obj2.Name.ToLower(), obj2.ObjectTransformed);



                if (obj2 is Exit)
                {
                    (obj2 as Exit).IsLocked = false;
                }
                return true;
            }

            return false;
        }

        public static string Look(GameObject obj, Preposition preposition)
        {
            string returnString = "";
            if (preposition == Preposition.I)
            {
                if ((obj as GameObjectContainer) != null)
                {
                    if ((obj as GameObjectContainer).Objects.Count > 0)
                    {
                        foreach (var o in (obj as GameObjectContainer).Objects)
                        {
                            returnString += o.Value.Name + "\n";
                        }
                        return returnString;
                    }
                    else
                    {
                        return "Den är tom...";
                    }
                }
                else
                {
                    return "Det går inte att titta inuti den";
                }
            }
            else if (preposition == Preposition.På)
            {
                return obj.Description;
            }

            return "Jag förstod inte vad du menade...";
        }

        public static void Go(Player player, Room room, Direction direction)
        {
            if (room.TryFindExitFromDirection(room, direction, out Exit exit))
            {
                exit.GoThrough(player);
            }
            else
            {
                Console.WriteLine("Du slog i en vägg...");
                Console.ReadLine();
            }
        }

    }
}