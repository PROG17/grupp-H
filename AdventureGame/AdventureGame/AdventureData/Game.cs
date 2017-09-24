using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    class Game
    {

        public Dictionary<string, Room> Rooms { get; set; }
        public Player Player { get; set; }

        public Game()
        {
            Player = new Player
            {
                Name = "Andreas",
                Description = "Aktuell spelare",
            };
            Rooms = new Dictionary<string, Room>();

            var start = new Room
            {
                Name = "Källare",
                Description = "Du är i en mörk källare",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false,
            };
            var end = new Room
            {
                Name = "Baksidan av huset",
                Description = "Du står på baksidan av huset, luften är frisk",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = true
            };
            var trasigTunna = new ObjectContainer
            {
                Name = "trasig tunna",
                Description = "en trasig tunna, träflisor och gjutjärnsringar. Kanske ligger det något bland spillrorna",
                DirectionalPosition = Direction.Syd,
                ObjectTransformed = null
            };
            var tunna = new Object
            {
                Name = "tunna",
                Description = "en stor tunna i trä och gjutjärn. Tunnan går inte att öppna utan något verktyg...",
                CanUseWith = "HAMMARE",
                ObjectTransformed = trasigTunna,
                DirectionalPosition = Direction.Syd


            };



            var dorr = new Exit
            {
                Name = "trädörr",
                Description = "en stor trädörr",
                GoesTo = end,
                IsLocked = true,
                DirectionalPosition = Direction.Norr,

            };

            var dorr2 = new Exit
            {
                Name = "trädörr",
                Description = "en stor trädörr",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Syd,

            };

            var nyckel = new Object
            {
                Name = "nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                DirectionalPosition = Direction.Syd,
                ObjectTransformed = null
            };

            var hammer = new Object
            {
                Name = "hammare",
                Description = "en robust hammare",
                CanUseWith = tunna.Name,
                DirectionalPosition = Direction.Väst,
                ObjectTransformed = null
            };

            Player.PlayerLocation = start;
            trasigTunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Objects.Add(dorr.Name.ToLower(), dorr);
            start.Objects.Add(tunna.Name.ToLower(), tunna);
            start.Objects.Add(hammer.Name.ToLower(), hammer);
            //start.Objects.Add(nyckel.Name.ToLower(), nyckel);
            //tunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Exits.Add(dorr.Name.ToLower(), dorr);
            end.Exits.Add(dorr2.Name.ToLower(), dorr2);
            end.Objects.Add(dorr2.Name.ToLower(), dorr2);

            Rooms.Add("start", start);
            Rooms.Add("end", end);

        }

        public static bool ValidateSentence(string[] split, out string[] strings)
        {
            if (split.Length > 0 && split.Length < 5)
            {
                if (split.Length == 1)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        strings = split;
                        return true;
                    }
                }
                else if (split.Length == 2)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (action == Action.Gå || action == Action.Titta)
                        {
                            if (Direction.TryParse(split[1], true, out Direction dir))
                            {
                                strings = split;
                                return true;
                            }
                        }
                        else if (action == Action.Släpp || action == Action.Ta || action == Action.Inspektera)
                        {
                            strings = split;
                            return true;
                        }

                    }
                }
                else if (split.Length == 3)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (Preposition.TryParse(split[1], true, out Preposition prepos))
                        {
                            strings = split;
                            return true;
                        }
                    }
                }
                else if (split.Length == 4)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (Preposition.TryParse(split[2], true, out Preposition prepos))
                        {
                            strings = split;
                            return true;
                        }
                    }
                }
            }
            strings = null;
            return false;
        }

        private static void CategorizeSentenceStrings(string[] split, out string actionStr, out string directionStr,
            out string objStr1, out string preposStr, out string objStr2)
        {
            actionStr = split[0];
            directionStr = null;
            objStr1 = null;
            objStr2 = null;
            preposStr = null;

            if (split.Length == 2)
            {
                if (Act.IsDirectionEnum(split[1]))
                {
                    directionStr = split[1];
                }
                else
                {
                    objStr1 = split[1];
                }
                preposStr = null;
                objStr2 = null;
            }
            else if (split.Length == 3)
            {
                preposStr = split[1];
                objStr1 = split[2];
                directionStr = null;
                objStr2 = null;
            }
            else if (split.Length == 4)
            {
                objStr1 = split[1];
                preposStr = split[2];
                objStr2 = split[3];
                directionStr = null;
            }
        }

        public bool Prompt()
        {
            Room currentRoom = Player.PlayerLocation;
            Console.WriteLine(currentRoom.Description);
            while (true)
            {
                currentRoom = Player.PlayerLocation;
                Console.Write("Vad vill du göra? ");
                // Spelare matar in handling
                string input = Console.ReadLine();

                // Splittar strängen inför validering och tilldelning
                var split = input.ToLower().Split(' ');

                // Strängen skickas in i "ValidateSentence" som returnerar "true" om
                // strängen matchar tillåtna syntax, i annat fall returneras "false"
                // och spelaren får prova en annan mening.
                if (!Game.ValidateSentence(split, out string[] sentence))
                {
                    Console.WriteLine("Jag förstod inte vad du menade...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                // Skickar in input från spelaren och får tillbaka rätt ord i rätt "ordklass"
                CategorizeSentenceStrings(
                    split,
                    out string actionStr,
                    out string directionStr,
                    out string objStr1,
                    out string preposStr,
                    out string objStr2);

                // Tilldelar "action" den handling som spelaren skrivit in.
                Action action = (Action)Enum.Parse(typeof(Action), actionStr, true);

                // Använder action som parameter i switch-sats nedan. Varje case är en giltig handling
                switch (action)
                {
                    case Action.Titta:

                        string startString = "Du ser ";

                        // Beroende på hur många ord som skrivits in behandlas orden på olika sätt
                        if (directionStr != null)
                        {
                            if (Direction.TryParse(directionStr, true, out Direction lookDirection))
                            {
                                string writeOut =
                                    currentRoom.TryFindObjectInDirection(currentRoom, lookDirection, out GameObject roomobj)
                                        ? roomobj.Name.ToLower()
                                        : "en vägg";
                                Console.WriteLine($"{startString}{writeOut}");
                                Console.ReadLine();
                            }
                        }
                        else if (objStr1 != null)
                        {

                            if (currentRoom.Objects.ContainsKey(objStr1))
                            {
                                Console.WriteLine(
                                    Act.Look(currentRoom.Objects[objStr1],
                                        (Preposition)Enum.Parse(typeof(Preposition), preposStr, true)));
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine($"Du ser ingen {objStr1}");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine(currentRoom.Description);
                            Console.ReadLine();
                        }

                        break;
                    case Action.Inspektera:

                        if (objStr1 != null)
                        {

                            if (currentRoom.Objects.ContainsKey(objStr1))
                            {
                                Console.WriteLine(
                                    Act.Inspect(currentRoom.Objects[objStr1]));
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine($"Du ser ingen {objStr1}");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine(currentRoom.Description);
                            Console.ReadLine();
                        }

                        break;
                    case Action.Använd:

                        if (split.Length == 4)
                        {
                            bool hasOject1 = Player.Objects.TryGetValue(objStr1, out GameObject obj1);
                            bool hasObject2 = currentRoom.Objects.TryGetValue(objStr2, out GameObject obj2);
                            if (hasOject1 && hasObject2)
                            {

                                if (Act.Use(Player, obj1, obj2))
                                {
                                    Console.WriteLine("Det gick!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Det gick inte...");
                                    Console.ReadLine();
                                }
                            }
                            else if (!hasOject1 && !hasObject2)
                            {
                                Console.WriteLine($"Det finns ingen {objStr1} eller {objStr2}");
                                Console.ReadLine();
                            }
                            else if (!hasObject2)
                            {
                                Console.WriteLine($"Det finns ingen {objStr2}");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine($"Du har ingen {objStr1}...");
                                Console.ReadLine();
                            }
                        }

                        break;
                    case Action.Släpp:
                        if (Player.Objects.TryGetValue(objStr1, out GameObject obj))
                        {
                            if (Act.Drop(Player, obj, currentRoom))
                            {
                                Console.WriteLine("Det gick!");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Det gick inte...");
                                Console.ReadLine();
                            }
                        }
                        break;
                    case Action.Gå:

                        if (!Enum.TryParse(directionStr, true, out Direction walkDirection))
                        {
                            Console.WriteLine("Jag förstod inte vad du menade...");
                            Console.ReadLine();
                            Console.Clear();
                            continue;
                        }

                        if (currentRoom.TryFindExitFromDirection(currentRoom, walkDirection, out Exit walkExit))
                        {
                            if (walkExit.IsLocked)
                            {
                                Console.WriteLine("Dörren är låst");
                                Console.ReadLine();
                                Console.Clear();
                                continue;
                            }
                            else
                            {
                                Act.Go(Player, currentRoom, walkDirection);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du gick in i en vägg...");
                            Console.ReadLine();
                            Console.Clear();
                            continue;
                        }

                        break;
                    case Action.Ta:

                        // Om spelaren vill ta ett objekt ur ett annat objekt...
                        if (Enum.TryParse(preposStr, true, out Preposition prep))
                        {
                            Act.Get(Player, objStr1, objStr2);
                            Console.WriteLine($"Du tog {Player.Objects[objStr1].Name}");
                            Console.ReadLine();
                        }

                        // Om spelaren vill ta ett objekt i rummet
                        if (currentRoom.Objects.TryGetValue(objStr1, out GameObject takeObject))
                        {
                            Act.Get(Player, takeObject);
                            Console.WriteLine($"Du tog {takeObject.Name}");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"Det finns ingen {objStr1} här...");
                            Console.ReadLine();
                        }
                        break;
                    case Action.Avsluta:
                        while (true)
                        {
                            Console.WriteLine("Är du säker på att du vill avsluta?");
                            Console.Write("J/N: ");
                            string answer = Console.ReadLine();
                            if (answer.ToUpper() == "N")
                            {
                                Console.WriteLine("Okej, spelet fortsätter!");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                            else if (answer.ToUpper() == "J")
                            {
                                Console.WriteLine("Hej Då!");
                                Console.ReadLine();
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("Fel! Vänligen mata in J/N...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (input.ToUpper() == "HJÄLP")
                {
                    Console.WriteLine("\n[GÅ]+[NORR|SYD|ÖST|VÄST]" +
                                      "\n\tGå i riktining\n" +
                                      "\n[TA|PLOCKA UPP] + [<FÖREMÅL>]" +
                                      "\n\tTar upp ett föremål\n" +
                                      "\n[SLÄPP] + [<FÖREMÅL>]" +
                                      "\n\tSläpper föremålet\n" +
                                      "\n[TITTA] | [TITTA] + [FÖREMÅL]" +
                                      "\n\tTittar på uppplockat föremål eller på föremål i rummet\n" +
                                      "\n[ANVÄND] + [<FÖREMÅL>] + [PÅ] + [<FÖREMÅL> | <UTGÅNG>]" +
                                      "\n\tAnvänder föremål på annat föremål/utgång\n");
                }
            }

            return true;
        }
    }
}
