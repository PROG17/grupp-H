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

            // Skapar rum
            var start = new Room
            {
                Name = "Källare",
                Description = "Du är i en mörk källare",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false,
            };
            var rumTillÖst = new Room
            {
                Name = "Gammalt badrum",
                Description = "Du är i ett gammalt, sedan länge oanvänt, badrum. Det är svart mögel på väggarna",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false
            };
            var rumTillVäst = new Room
            {
                Name = "Maskinrum",
                Description = "Du är i ett bullrigt maskinrum. Det luktar diesel...",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false
            };
            var room3 = new Room
            {
                Name = "Baksidan av huset",
                Description = "Du står på baksidan av huset, luften är frisk",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false
            };
            var end = new Room
            {
                Name = "En lektionssal",
                Description = "En gammal lektionssal med dålig ventilation. I ena hörnet ligger resterna " +
                              "av den senaste klassen som inte uplevde någon aha uplevelse.",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = true
            };

            // Ett gäng dörrar
            var dorr = new Exit
            {
                Name = "en trädörr",
                Description = "en stor trädörr",
                GoesTo = room3,
                IsLocked = true,
                DirectionalPosition = Direction.Norr,

            };

            var dorr2 = new Exit
            {
                Name = "en trädörr",
                Description = "en stor trädörr",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Syd,
            };

            var dorr3 = new Exit
            {
                Name = "en plåtdörr",
                Description = "en bucklig plåtdörr",
                GoesTo = rumTillÖst,
                IsLocked = false,
                DirectionalPosition = Direction.Öst,
            };
            var dorr4 = new Exit
            {
                Name = "en plåtdörr",
                Description = "en bucklig plåtdörr",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Väst,
            };
            var dorr5 = new Exit
            {
                Name = "ett hål i väggen",
                Description = "ett stort hål i väggen, förmodligen en misslyckad renovering...",
                GoesTo = rumTillVäst,
                IsLocked = false,
                DirectionalPosition = Direction.Väst,
            };
            var dorr6 = new Exit
            {
                Name = "ett hål i väggen",
                Description = "ett stort hål i väggen, förmodligen en misslyckad renovering...",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Öst,
            };

            // Objekt
            var trasigTunna = new ObjectContainer
            {
                Name = "en trasig tunna",
                Description = "en trasig tunna, träflisor och gjutjärnsringar. Kanske ligger det något bland spillrorna",
                DirectionalPosition = null,
                ObjectTransformed = null
            };
            var tunna = new Object
            {
                Name = "en tunna",
                Description = "en stor tunna i trä och gjutjärn. Tunnan går inte att öppna utan något verktyg...",
                CanUseWith = "en hammare",
                ObjectTransformed = trasigTunna,
                DirectionalPosition = null
            };

            var kaffe = new Object
            {
                Name = "kaffe",
                Description = "En kopp kaffe med lagom mängd mjölk. Sägs kunna blidka den argaste läraren.",
                CanUseWith = null,
                ObjectTransformed = null,
                DirectionalPosition = null,
            };
            
            var fredrik = new Object
            {
                Name = "Fredrik Haglund",
                Description = "En lärare på nackademin i sina bästa år. ",
                CanUseWith = "en hammare",
                DirectionalPosition = Direction.Norr
            };
            var argaFredrik = new Object
            {
                Name = "Arg Fredrik Haglund",
                Description = "En arg Fredrik. Den arga fredrik håller en dator i handen och håller fingret mot en knapp. Om du gjort Fredrik arg " +
                              "så loggas ditt namn och mailas efter programkörning till Fredrik.",
                CanUseWith = "kaffe",
                ObjectTransformed = fredrik,
                DirectionalPosition = Direction.Norr
            };
            fredrik.ObjectTransformed = argaFredrik;

            var bokhylla = new Object
            {
                Name = "en bokhylla",
                Description = "En bokhylla med några mögliga böcker...",
                CanUseWith = "en hammare",
                ObjectTransformed = null,
                DirectionalPosition = Direction.Syd
            };

            var soptunna = new ObjectContainer
            {
                Name = "en soptunna",
                Description = "En soptunna som ser ut att vara full av grejer",
                CanUseWith = null,
                ObjectTransformed = null,
                DirectionalPosition = null
            };
            var nyckel = new Object
            {
                Name = "en nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                DirectionalPosition = null,
                ObjectTransformed = null
            };

            var hammer = new Object
            {
                Name = "en hammare",
                Description = "en robust hammare",
                CanUseWith = tunna.Name,
                DirectionalPosition = null,
                ObjectTransformed = null
            };

            Player.PlayerLocation = start;
            trasigTunna.Objects.Add(nyckel.Key.ToLower(), nyckel);
            soptunna.Objects.Add(hammer.Key.ToLower(), hammer);
            start.Objects.Add(dorr.Key.ToLower(), dorr);
            start.Objects.Add(tunna.Key.ToLower(), tunna);
            start.Exits.Add(dorr.Key.ToLower(), dorr);
            start.Exits.Add(dorr3.Key.ToLower(), dorr3);
            start.Objects.Add(dorr3.Key.ToLower(), dorr3);
            start.Exits.Add(dorr5.Key.ToLower(), dorr5);
            start.Objects.Add(dorr5.Key.ToLower(), dorr5);
            rumTillÖst.Exits.Add(dorr4.Key.ToLower(), dorr4);
            rumTillÖst.Objects.Add(dorr4.Key.ToLower(), dorr4);
            rumTillÖst.Objects.Add(bokhylla.Key.ToLower(), bokhylla);
            rumTillVäst.Exits.Add(dorr6.Key.ToLower(), dorr6);
            rumTillVäst.Objects.Add(dorr6.Key.ToLower(), dorr6);
            rumTillVäst.Objects.Add(soptunna.Key.ToLower(), soptunna);
            end.Exits.Add(dorr2.Key.ToLower(), dorr2);
            end.Objects.Add(dorr2.Key.ToLower(), dorr2);

            Rooms.Add("start", start);
            Rooms.Add("end", end);
            Rooms.Add("rum1", rumTillÖst);
            Rooms.Add("rum2", rumTillVäst);

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
            Console.WriteLine(currentRoom.GetContentAsString());

            while (true)
            {
                currentRoom = Player.PlayerLocation;

                if (currentRoom.IsEndPoint)
                {
                    Console.WriteLine("Grattis! Du klarade spelet!");
                    return false;
                }

                Console.Write("\nVad vill du göra? ");
                // Spelare matar in handling
                string input = Console.ReadLine();
                Console.WriteLine();

                // Splittar strängen inför validering och tilldelning
                var split = input.ToLower().Split(' ');

                // Strängen skickas in i "ValidateSentence" som returnerar "true" om
                // strängen matchar tillåtna syntax, i annat fall returneras "false"
                // och spelaren får prova en annan mening.
                if (!Game.ValidateSentence(split, out string[] sentence))
                {
                    Console.WriteLine("Jag förstod inte vad du menade...");

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

                        // Om användaren vill titta åt en riktning
                        if (directionStr != null)
                        {
                            if (Direction.TryParse(directionStr, true, out Direction lookDirection))
                            {
                                string writeOut =
                                    currentRoom.TryFindObjectInDirection(currentRoom, lookDirection, out GameObject roomobj)
                                        ? roomobj.Name.ToLower()
                                        : "en vägg";
                                Console.WriteLine($"{startString}{writeOut}");

                            }
                        }
                        // Om användaren vill titta på ett objekt
                        else if (objStr1 != null)
                        {

                            if (currentRoom.Objects.ContainsKey(objStr1))
                            {
                                Console.WriteLine(
                                    Act.Look(currentRoom.Objects[objStr1],
                                        (Preposition)Enum.Parse(typeof(Preposition), preposStr, true)));

                            }
                            else if (objStr1.Contains("ficka"))
                            {
                                //Console.WriteLine(Player.LookInPocket());
                                Console.WriteLine(Player.GetContentAsString());

                            }
                            else
                            {
                                Console.WriteLine($"Du ser ingen {objStr1}");

                            }
                        }
                        // Om användaren bara skrivit "Titta" så ges en beskrivning av rummet
                        else
                        {
                            Console.WriteLine(currentRoom.GetContentAsString());

                        }

                        break;
                    // "Inspektera" ger en mer detaljerad beskrivning av ett objekt
                    case Action.Inspektera:

                        if (objStr1 != null)
                        {
                            if (objStr2 != null && objStr2 != "ficka")
                            {
                                if (currentRoom.Objects.ContainsKey(objStr1))
                                {
                                    Console.WriteLine(
                                        Act.Inspect(currentRoom.Objects[objStr1]));

                                }
                                else
                                {
                                    Console.WriteLine($"Du ser ingen {objStr1}");

                                }
                            }
                            else if (objStr1.Contains("ficka"))
                            {
                                //Console.WriteLine(Player.LookInPocket());
                                Console.WriteLine(Player.GetContentAsString());

                            }
                            else if (objStr2 == "ficka")
                            {
                                if (Player.Objects.ContainsKey(objStr1))
                                {
                                    Console.WriteLine(
                                        Act.Inspect(Player.Objects[objStr1]));

                                }
                                else
                                {
                                    Console.WriteLine($"Du har ingen {objStr1} i ficka...");

                                }
                            }
                            else
                            {
                                if (currentRoom.Objects.ContainsKey(objStr1))
                                {
                                    Console.WriteLine(
                                        Act.Inspect(currentRoom.Objects[objStr1]));

                                }
                                else
                                {
                                    Console.WriteLine($"Du ser ingen {objStr1}");

                                }
                            }


                        }
                        else
                        {
                            Console.WriteLine(currentRoom.Description);

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

                                }
                                else
                                {
                                    Console.WriteLine("Det gick inte...");

                                }
                            }
                            else if (!hasOject1 && !hasObject2)
                            {
                                Console.WriteLine($"Det finns ingen {objStr1} eller {objStr2}");

                            }
                            else if (!hasObject2)
                            {
                                Console.WriteLine($"Det finns ingen {objStr2}");

                            }
                            else
                            {
                                Console.WriteLine($"Du har ingen {objStr1}...");

                            }
                        }

                        break;
                    case Action.Släpp:
                        if (Player.Objects.TryGetValue(objStr1, out GameObject obj))
                        {
                            if (Act.Drop(Player, obj, currentRoom))
                            {
                                Console.WriteLine("Det gick!");

                            }
                            else
                            {
                                Console.WriteLine("Det gick inte...");

                            }
                        }
                        break;
                    case Action.Gå:

                        if (!Enum.TryParse(directionStr, true, out Direction walkDirection))
                        {
                            Console.WriteLine("Jag förstod inte vad du menade...");

                            Console.Clear();
                            continue;
                        }

                        if (currentRoom.TryFindExitFromDirection(currentRoom, walkDirection, out Exit walkExit))
                        {
                            if (walkExit.IsLocked)
                            {
                                Console.WriteLine("Dörren är låst");

                                Console.Clear();
                                continue;
                            }
                            else
                            {
                                if (Act.Go(Player, currentRoom, walkDirection))
                                {
                                    currentRoom = Player.PlayerLocation;
                                    Console.Clear();
                                    Console.WriteLine(currentRoom.GetContentAsString());
                                }
                                else
                                {
                                    Console.WriteLine("Du slog i en vägg...");

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du gick in i en vägg...");

                            continue;
                        }

                        break;
                    case Action.Ta:

                        // Om spelaren vill ta ett objekt ur ett annat objekt...
                        if (Enum.TryParse(preposStr, true, out Preposition prep))
                        {
                            Act.Get(Player, objStr1, objStr2);
                            Console.WriteLine($"Du tog {Player.Objects[objStr1].Name}");

                        }
                        // Om spelaren vill ta ett objekt i rummet
                        else if (currentRoom.Objects.TryGetValue(objStr1, out GameObject takeObject))
                        {
                            Act.Get(Player, takeObject);
                            Console.WriteLine($"Du tog {takeObject.Name}");

                        }
                        else
                        {
                            Console.WriteLine($"Det finns ingen {objStr1} här...");

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

                                Console.Clear();
                                break;
                            }
                            else if (answer.ToUpper() == "J")
                            {
                                Console.WriteLine("Hej Då!");

                                return false;
                            }
                            else
                            {
                                Console.WriteLine("Fel! Vänligen mata in J/N...");

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
