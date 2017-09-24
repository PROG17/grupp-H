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
        public static bool Get(Player player, string objToGet, string objGetFrom)
        {
            if (player.PlayerLocation.Objects.TryGetValue(objGetFrom, out GameObject container))
            {
                if ((container as ObjectContainer).Objects.TryGetValue(objToGet, out GameObject obj))
                {
                    player.Objects.Add(obj.Name, obj as Object);
                    (container as ObjectContainer).Objects.Remove(obj.Name);
                    return true;
                }
            }
            return false;
        }

        public static bool Drop(Player player, GameObject obj, Room room)
        {
            if (player.Objects.ContainsKey(obj.Name) && !room.Objects.ContainsKey(obj.Name))
            {
                room.Objects.Add(obj.Name, obj);
                player.Objects.Remove(obj.Name);
                return true;
            }
            return false;

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
                if ((obj2 is Exit))
                {
                    (obj2 as Exit).IsLocked = false;
                }
                else
                {
                    player.PlayerLocation.Objects.Remove(obj2.Name);
                    player.PlayerLocation.Objects.Add(obj2.Name.ToLower(), obj2.ObjectTransformed);
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
                if ((obj as ObjectContainer) != null)
                {
                    if ((obj as ObjectContainer).Objects.Count > 0)
                    {
                        returnString = "Du tittar i den och ser...\n";
                        foreach (var o in (obj as ObjectContainer).Objects)
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
                return obj.Name;
            }

            return "Jag förstod inte vad du menade...";
        }

        public static string Inspect(GameObject obj)
        {
            return obj.Description;
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

        public static bool IsPrepositionEnum(string str)
        {
            return Enum.TryParse(str, true, out Preposition prep);
        }
        public static bool IsDirectionEnum(string str)
        {
            return Enum.TryParse(str, true, out Direction dir);
        }
        public static bool IsActionEnum(string str)
        {
            return Enum.TryParse(str, true, out Action act);
        }
    }
}