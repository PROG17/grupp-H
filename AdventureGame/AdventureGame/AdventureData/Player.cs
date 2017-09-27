using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Player : GameObjectsHolder
    {
        // Det aktuella rummet spelaren befinner sig i
        public Room PlayerLocation { get; set; }

        // En koll om spelare lever
        public bool IsAlive { get; internal set; } = true;

        // Overridad metod från basklass som skriver ut innehållet från "fickan"
        public override string GetContentAsString()
        {
            if (Objects.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("I din ficka ligger det:\n");
                sb.Append(base.GetContentAsString());
                return sb.ToString();
            }
            else
            {
                return ("Den är tom...");
            }
        }

        //---------------------------METODER FÖR INTERAKTION ----------------------------------

        // Tar ett objekt
        public string Get(string objStr)
        {
            if (PlayerLocation.Objects.TryGetValue(objStr, out GameObject obj))
            {
                if (obj.IsGetable)
                {
                    obj.DirectionalPosition = null;
                    PlayerLocation.Objects.Remove(obj.Key);
                    Objects.Add(obj.Key, obj);
                    return $"Du la {obj.Name} i fickan...";
                }
                return "Den gick inte att plocka upp...";
            }
            return $"Det finns ingen \"{objStr}\"";
        }

        // Tar ett objekt ur ett annat objekt
        public string Get(string toGetStr, string getFromStr)
        {
            if (PlayerLocation.Objects.TryGetValue(getFromStr, out GameObject container))
            {
                if ((container as ObjectContainer).Objects.TryGetValue(toGetStr, out GameObject obj))
                {
                    if (obj.IsGetable)
                    {
                        Objects.Add(obj.Key, obj as Object);
                        (container as ObjectContainer).Objects.Remove(obj.Key);
                        return $"Du la {obj.Name} i fickan.";
                    }

                }
                return $"Det finns ingen \"{toGetStr}\" där i.";
            }
            return "Den gick inte att plocka upp...";
        }

        // Släpper ett objekt och placerar det i aktuella rummet
        public string Drop(string objStr)
        {
            if (Objects.TryGetValue(objStr, out GameObject obj))
            {
                Objects.Remove(obj.Key);
                PlayerLocation.Objects.Add(obj.Key, obj);
                return $"Du släppte {obj.Name} på marken...";
            }
            return $"Du har ingen \"{obj.Key}\"...";
        }

        // Använder objekt med annat objekt, vilket byter ut objekt2 i aktuellt rum 
        // till inneboende objektvariabel "ObjectTransformed".
        // Om objekt2 är en dörr så ändras inneboende boolen "IsLocked" till true eller false, 
        // beroende på aktuellt värde.
        public string Use(string objStr1, string objStr2)
        {
            bool hasOject1 = Objects.TryGetValue(objStr1, out GameObject objToUse);
            bool hasObject2 = PlayerLocation.Objects.TryGetValue(objStr2, out GameObject objUseOn);
            if (hasOject1 && hasObject2)
            {
                if (objToUse.CanUseWith.Contains(objUseOn.Key))
                {
                    if (objUseOn.KillsOnUse.Contains(objToUse))
                    {
                        IsAlive = false;
                        return "AAAAHHHHHHHHHHHHHH!";
                    }
                    if ((objUseOn is Exit))
                    {
                        if ((objUseOn as Exit).IsLocked)
                        {
                            (objUseOn as Exit).IsLocked = false;
                            return "Du låste upp dörren.";
                        }
                        else
                        {
                            (objUseOn as Exit).IsLocked = true;
                            return "Du låste dörren.";
                        }
                    }
                    else
                    {
                        if (objToUse.CanUseWith.Contains(objUseOn.Key))
                        {
                            PlayerLocation.Objects.Remove(objUseOn.Key);
                            PlayerLocation.Objects.Add(objUseOn.Key, objUseOn.ObjectTransformed);
                            if (objUseOn.DropsItemOnUse)
                            {
                                (objUseOn as GameObjectsHolder).DropFirstItem(PlayerLocation);
                                return $"Du använde {objToUse.Key} på {objUseOn.Name}\nNågonting ramlade till marken...";
                            }
                            return $"Du använde {objToUse.Key} på {objUseOn.Name}";
                        }
                        return $"Det gick inte att använda {objToUse.Key} på {objUseOn.Name}";
                    }
                }
                return $"Det går inte att använda {objToUse.Key} på {objUseOn.Name}.";
            }
            else if (!hasOject1 && !hasObject2)
            {
                return $"Det finns ingen {objStr1} eller {objStr2}";
            }
            else if (!hasObject2)
            {
                return $"Det finns ingen {objStr2}";
            }
            else
            {
                return $"Du har ingen {objStr1}...";
            }
        }

        // Returnerar rumbeskrivning och innehåll
        public string Look()
        {
            return PlayerLocation.GetContentAsString();
        }

        // Returnerar innehåll i objekt
        public string LookIn(string obj)
        {
            if (obj.Contains("ficka"))
            {
                return this.GetContentAsString();
            }
            if (PlayerLocation.Objects.TryGetValue(obj, out GameObject gameObject))
            {
                if (gameObject is ObjectContainer)
                {
                    return (gameObject as ObjectContainer).GetContentAsString();
                }
                else
                {
                    return "Det går inte att titta inuti den.";
                }
            }
            return "Va?";
        }

        // Returnerar namnet på objekt 
        public string LookAt(string obj)
        {
            if (PlayerLocation.Objects.TryGetValue(obj, out var gameObject))
            {
                return $"Det är {gameObject.Name}";
            }
            return $"Det finns ingen \"{obj}\" att titta på.";
        }

        // Returnerar namnet på objektet i riktining
        public string LookTo(Direction direction)
        {
            if (PlayerLocation.TryFindObjectInDirection(PlayerLocation, direction, out GameObject gameObject))
            {
                return $"Till {direction.ToString()} ser du {gameObject.Name}";
            }
            return $"Till {direction.ToString()} ser du en vägg.";
        }

        // Skriver ut mer detaljerad beskrivning av ett objekt
        public string Inspect(string obj)
        {
            if (obj != null)
            {
                if (PlayerLocation.Objects.TryGetValue(obj, out GameObject gameObject))
                {
                    return $"Du inspekterar {gameObject.Name}.\n{gameObject.Description}";
                }
                return $"Det finns ingen \"{obj}\" att inspektera.";
            }
            return "Inspektera vad?";
        }

        // Skriver ut mer detaljerad beskrivning av ett objekt som är inuti ett annat objekt
        public string InspectIn(string obj, string objContainer)
        {
            if (objContainer.Contains("ficka"))
            {
                if (Objects.TryGetValue(obj, out GameObject gameObject))
                {
                    return $"Det är {gameObject.Description}";
                }
                return $"Det finns ingen \"{obj}\" att inspektera.";
            }
            else if (PlayerLocation.Objects.TryGetValue(obj, out GameObject container))
            {
                if (container is ObjectContainer)
                {
                    if (PlayerLocation.Objects.TryGetValue(obj, out GameObject gameObject))
                    {
                        return $"Du inspekterar {gameObject.Name}.\n{gameObject.Description}";
                    }
                }
                else
                {
                    return "Det går inte att titta inuti den.";
                }
            }
            return "Va?";
        }

        // Ändrar PlayerLocation till det rum som finns i riktning, skickar ut bool som true.
        // Om inet rum finns returneras en sträng om att spelaren gick in i
        // en vägg, eller ett objekt som fanns i riktningen, skickar ut bool som false.
        public string Go(Direction direction, out bool success)
        {
            if (PlayerLocation.TryFindExitFromDirection(PlayerLocation, direction, out Exit exit))
            {
                exit.GoThrough(this);
                success = true;
                return $"{PlayerLocation.GetContentAsString()}";
            }
            else if (PlayerLocation.TryFindObjectInDirection(PlayerLocation, direction, out GameObject obj))
            {
                success = false;
                return$"Du gick in i {obj.Name}.";
            }
            success = false;
            return $"Du gick in i en vägg.";
        }

        // Returnerar sträng som ett objekt har i inneboende "Dialog"
        public string Talk(string objStr)
        {
            if(PlayerLocation.Objects.TryGetValue(objStr, out GameObject obj))
            {
                if (obj is Person)
                {
                    return $"{obj.Name} säger:\n{(obj as Person).Dialog}";
                }
                return $"{obj.Key} svarar inte.";
            }
            return "Vem?";
        }
    }
}
