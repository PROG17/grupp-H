using System;
using System.CodeDom.Compiler;
using System.Dynamic;
using System.Xml.Linq;

namespace AdventureGame.AdventureData.Interact
{
    public static class Act
    {
        public static bool Get(Player player, GameObject obj)
        {
            if (obj.IsGetable)
            {
                player.Objects.Add(obj.Key, obj as Object);
                player.PlayerLocation.Objects.Remove(obj.Key);
                return true;
            }
            return false;
        }
        public static bool Get(Player player, string objToGet, string objGetFrom)
        {
            if (player.PlayerLocation.Objects.TryGetValue(objGetFrom, out GameObject container))
            {
                if ((container as ObjectContainer).Objects.TryGetValue(objToGet, out GameObject obj))
                {
                    if (obj.IsGetable)
                    {
                        player.Objects.Add(obj.Key, obj as Object);
                        (container as ObjectContainer).Objects.Remove(obj.Key);
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public static bool Drop(GameObjectsHolder holder, GameObject obj, Room room)
        {
            if (holder.Objects.ContainsKey(obj.Key) && !room.Objects.ContainsKey(obj.Key))
            {
                room.Objects.Add(obj.Key, obj);
                holder.Objects.Remove(obj.Key);
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

            if (obj1.CanUseWith.Contains(obj2.Name))
            {
                if ((obj2 is Exit))
                {
                    (obj2 as Exit).IsLocked = false;
                }
                else
                {
                    player.PlayerLocation.Objects.Remove(obj2.Key);
                    player.PlayerLocation.Objects.Add(obj2.Key, obj2.ObjectTransformed);
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
                    return (obj as ObjectContainer).GetContentAsString();
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

        public static bool Go(Player player, Room room, Direction direction)
        {
            if (room.TryFindExitFromDirection(room, direction, out Exit exit))
            {
                exit.GoThrough(player);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Talk(Person obj)
        {
            if (obj.Dialog != null)
            {
                return $"\"{obj.Dialog}\"";
            }
            return "Den svarade inte...";
        }

        public static void Hit(Player player, GameObject objToUse, GameObject objToHit)
        {
            if (objToHit.IsHitable)
            {
                if (objToHit is Person)
                {
                    if ((objToHit as Person).HitsBack)
                    {
                        Console.WriteLine($"{objToHit.Name} slog tillbaka och du dog...");
                        player.IsAlive = false;
                    }
                    else
                    {
                        Console.WriteLine($"{objToHit.Name} tog smällen och tittar besviket på dig...");
                    }
                }
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