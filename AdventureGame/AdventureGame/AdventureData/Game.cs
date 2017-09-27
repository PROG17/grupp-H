using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class Game
    {
        public Player Player { get; set; }

        public Game()
        {
            Player = new Player
            {
                Name = "",
                Description = "Aktuell spelare",
            };

            // Skapar rum
            var start = new Room
            {
                Name = "Lobbyn",
                Description = "Du är i lobbyn till Nackademin. Men det ser inte ut som du mins det." +
                              "\n När du tänker efter så undrar du om man kan kalla ett " +
                              "litet rum med fyra väggar för lobby?",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false,
            };
            var badrum = new Room
            {
                Name = "Badrum",
                Description = "Du är i ett av skolans badrum. Undrar när det blev städat här senast?\n " +
                              "Det luktar byggklassare och gamla strumpor."

                              ,
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false
            };
            var ventilationsrum = new Room
            {
                Name = "Ventilationsrum",
                Description = "Du är i ett bullrigt maskinrum. Kanske är det det här som kallas ventilation " +
                              "i den här byggnaden? Det luktar diesel...",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false
            };


            var kok = new Room
            {
                Name = "Kök",
                Description = "Längs med väggarna står det förvånansvärt många microvågsugnar. " +
                              "Resterna av Java klassarnas lunch ligger i högar över hela rummet." +
                              "I hörnet står en kran och läcker, ",
                IsEndPoint = false

            };
            var uppehallsrum = new Room
            {
                Name = "Uppehållsrum",
                Description = "Priserna i kaffeterian hade tillslut blivit för mycket för eleverna på Nackademin. \n" +
                              "Vad som var kvar av kaffeterian går knappt att urskilja mellan de " +
                              "trasiga bord och stolar som blockerar dess ingång.",
                IsEndPoint = false
            };
            var lektionssal = new Room
            {
                Name = "En lektionssal",
                Description = "En gammal lektionssal med dålig ventilation. I ena hörnet ligger resterna " +
                              "av den senaste klassen som \"inte upplevde någon aha uplevelse\".",
                IsEndPoint = false
            };
            var end = new Room
            {
                Name = "Rökrutan",
                Description = "Du står på baksidan av huset, luften är frisk. \n " +
                              "Eller nja, den var det tills du tände en cigg...",
                IsEndPoint = true
            };

            // Ett gäng dörrar
            var traDorrTillKok = new Exit
            {
                Name = "en trädörr",
                Description = "en stor trädörr",
                GoesTo = kok,
                IsLocked = false,
                DirectionalPosition = Direction.Väst,

            };

            var traDorrTillLobby = new Exit
            {
                Name = "en trädörr",
                Description = "en stor trädörr",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Öst,
            };

            var platDorrTillURum = new Exit
            {
                Name = "en plåtdörr",
                Description = "en bucklig plåtdörr",
                GoesTo = uppehallsrum,
                IsLocked = false,
                DirectionalPosition = Direction.Öst,
            };
            var platDorrTillLobby = new Exit
            {
                Name = "en plåtdörr",
                Description = "en bucklig plåtdörr",
                GoesTo = start,
                IsLocked = false,
                DirectionalPosition = Direction.Väst,
            };
            var halIVaggenBadrum = new Exit
            {
                Name = "en vägg med ett stort hål",
                Description = "ett stort hål i väggen, förmodligen en misslyckad renovering...",
                GoesTo = badrum,
                IsLocked = false,
                DirectionalPosition = Direction.Öst,
            };
            var halIVaggenURum = new Exit
            {
                Name = "en vägg med ett stort hål",
                Description = "ett stort hål i väggen, förmodligen en misslyckad renovering...",
                GoesTo = uppehallsrum,
                IsLocked = false,
                DirectionalPosition = Direction.Väst,
            };
            var hissDorrVent = new Exit
            {
                Name = "en hissdörr",
                Description = "En perfekt dörr för när man vill åka hiss med stil!\n " +
                              "Leder endast till våning 4 står det skrivet över dörren.",
                GoesTo = ventilationsrum,
                IsLocked = false,
                DirectionalPosition = Direction.Norr


            };
            var hissDorrURum = new Exit
            {
                Name = "en hissdörr",
                Description = "En perfekt dörr för när man vill åka hiss med stil!\n " +
                              "Leder endast till våning 0 står det skrivet över dörren.",
                GoesTo = uppehallsrum,
                IsLocked = false,
                DirectionalPosition = Direction.Norr

            };
            var dorrTillEnd = new Exit
            {
                Name = "en säkerhetsdörr",
                Description = "En säkerhetsdörr med ett lås gjort av lego, ett tuggummi, en hårnål och ett batteri. \n" +
                              "Låset som Mcguyver är baserad på.",
                GoesTo = end,
                IsLocked = true,
                DirectionalPosition = Direction.Väst
            };
            var dorrTillKlassRum = new Exit
            {
                Name = "en klassrummsdörr",
                Description = "ngt",
                GoesTo = lektionssal,
                IsLocked = false,
                DirectionalPosition = Direction.Väst
            };
            var dorrTillVent = new Exit
            {
                Name = "en klassrummsdörr",
                Description = "ngt",
                GoesTo = ventilationsrum,
                IsLocked = false,
                DirectionalPosition = Direction.Öst
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
                CanUseWith = { "hammare" },
                ObjectTransformed = trasigTunna,
                DirectionalPosition = null,
                IsGetable = false
            };

            var kaffe = new Object
            {
                Name = "en kaffe",
                Description = "En kopp kaffe med lagom mängd mjölk, även kallad gudarnas nektar.\n" +
                              "Ska enligt gammal grekisk mytologi kunna blidka den argaste läraren...",
                CanUseWith = { "fredrik" },
                ObjectTransformed = null,
                DirectionalPosition = null,
            };

            var fredrik = new Person
            {
                Name = "Fredrik",
                Description = "En lärare på nackademin i sina bästa år. ",
                CanUseWith = { "en hammare" },
                DirectionalPosition = Direction.Norr,
                Dialog = "Vet ni hur man blir rik genom objekt orientering? Genom arv! HAHAHA fattar ni?\n" +
                         "Den här då:\n" +
                         "Varför behöver java programmerare använda glasögon?\n" +
                         "För att dom inte c#! hahahahahahaha \n" +
                         "Eller ja den blir bättre på engelska.",
                DropsItemOnUse = true
            };
            var argaFredrik = new Person
            {
                Name = "en arg Fredrik",
                Description = "En arg Fredrik. Den arga fredrik håller en dator i handen och håller fingret mot \n" +
                              "en knapp. Om du gjort Fredrik arg " +
                              "så loggas ditt namn och mailas efter programkörning till Fredrik.",
                CanUseWith = { kaffe.Name },
                ObjectTransformed = fredrik,
                DirectionalPosition = Direction.Norr,
                Dialog = "Jasså du gillar inte mina skämt? Då är det dags för ett oförberett diagnostiskt test!\n" +
                         "Mohahaha jag kan göra hur många test jag vill!",
                DropsItemOnUse = true
            };
            fredrik.ObjectTransformed = argaFredrik;

            var bokhylla = new Object
            {
                Name = "en bokhylla",
                Description = "En bokhylla fylld med böcker om programmering",
                CanUseWith = { "en hammare" },
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
                CanUseWith = null,
                DirectionalPosition = null,
                ObjectTransformed = null
            };
            var bilnyckel = new Object
            {
                Name = "en bilnyckel",
                Description = "en bilnyckel med ett tesla emblem på",
                CanUseWith = null,
                ObjectTransformed = null
            };

            var hammer = new Object
            {
                Name = "en hammare",
                Description = "en robust hammare",
                CanUseWith = { tunna.Key, fredrik.Key, bokhylla.Key },
                DirectionalPosition = null,
                ObjectTransformed = null
            };
            var skruvmejsel = new Object
            {
                Name = "en skruvmejsel",
                Description = "en skruvmejsel med plasthandtag",
                CanUseWith = {"elskåp"},
                DirectionalPosition = null,
                ObjectTransformed = null
            };
            var elskap = new Object
            {
                Name = "ett elskåp",
                Description = "Förser rummet med ström.",
                CanUseWith = { "en hammare" },
                DirectionalPosition = Direction.Syd,
                ObjectTransformed = null,
                KillsOnUse = {skruvmejsel}
            };

            // Sätter startpunkt
            Player.PlayerLocation = start;

            // Lägger till objekt i andra objekt
            AddGameObjectsToContainer(argaFredrik, skruvmejsel);
            AddGameObjectsToContainer(fredrik, bilnyckel);
            AddGameObjectsToContainer(trasigTunna, nyckel);
            AddGameObjectsToContainer(soptunna, hammer);

            // Lägger till objekt i rummet "start"
            AddGameObjectsToRoom(start, traDorrTillKok);
            AddGameObjectsToRoom(start, tunna);
            AddGameObjectsToRoom(start, platDorrTillURum);

            // Lägger till objekt i rummet "uppehallsrum"
            AddGameObjectsToRoom(uppehallsrum, halIVaggenBadrum);
            AddGameObjectsToRoom(uppehallsrum, platDorrTillLobby);
            AddGameObjectsToRoom(uppehallsrum, hissDorrVent);

            // Lägger till objekt i rummet "badrum"
            AddGameObjectsToRoom(badrum, halIVaggenURum);
            AddGameObjectsToRoom(badrum, bokhylla);

            // Lägger till objekt i rummet "ventilationsrum"
            AddGameObjectsToRoom(ventilationsrum, hissDorrURum);
            AddGameObjectsToRoom(ventilationsrum, dorrTillKlassRum);
            AddGameObjectsToRoom(ventilationsrum, soptunna);

            // Lägger till objekt i rummet "kok"
            AddGameObjectsToRoom(kok, traDorrTillLobby);

            // Lägger till objekt i rummet "lektionssal"
            AddGameObjectsToRoom(lektionssal, dorrTillVent);
            AddGameObjectsToRoom(lektionssal, fredrik);
            AddGameObjectsToRoom(lektionssal, elskap);
            AddGameObjectsToRoom(lektionssal, dorrTillEnd);
        }

        // Metod där kommandon tas in från användaren och utvärderas samt 
        // kallar på lämplig metod och skriver ut resultat av kallad metod.
        public bool Prompt()
        {
            Room currentRoom = Player.PlayerLocation;
            Console.WriteLine(currentRoom.GetContentAsString());

            bool isPlaying = true;
            while (isPlaying)
            {
                currentRoom = Player.PlayerLocation;

                // Om användaren klarat spelet
                if (currentRoom.IsEndPoint)
                {
                    Console.WriteLine("Grattis! Du klarade spelet!");
                    return RestartGameDialog();
                }
                // Om användaren dör
                if (!Player.IsAlive)
                {
                    Console.WriteLine("Du dog...");
                    Console.ReadLine();
                    return RestartGameDialog();
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

                // Skapar enum för action till switch-sats nedan
                Action action = (Action)Enum.Parse(typeof(Action), actionStr, true);
                // Skapar Nullable-Enums av Direction och Preposition
                Direction? direction = Direction.TryParse(directionStr, true, out Direction dir)
                    ? (Direction?)dir
                    : null;
                Preposition? preposition = Preposition.TryParse(preposStr, true, out Preposition prep)
                    ? (Preposition?)prep
                    : null;

                // Använder action som parameter i switch-sats nedan. Varje case är en giltig handling
                switch (action)
                {
                    case Action.Titta:

                        if (preposition != null)
                        {
                            if (preposition == Preposition.I)
                            {
                                Console.WriteLine(Player.LookIn(objStr1));
                            }
                            else if (preposition == Preposition.På)
                            {
                                Console.WriteLine(Player.LookAt(objStr1));
                            }
                        }
                        else if (direction != null)
                        {
                            Console.WriteLine(Player.LookTo((Direction)direction));
                        }
                        else
                        {
                            Console.WriteLine(Player.Look());
                        }

                        break;
                    // "Inspektera" ger en mer detaljerad beskrivning av ett objekt
                    case Action.Inspektera:

                        if (preposition != null)
                        {
                            if (preposition == Preposition.I)
                            {
                                Console.WriteLine(Player.InspectIn(objStr1, objStr2));
                            }
                        }
                        else
                        {
                            Console.WriteLine(Player.Inspect(objStr1));
                        }
                        break;
                        // Om användaren försöker använda ett objekt med ett annat
                    case Action.Använd:
                        if (split.Length == 4)
                        {
                            Console.WriteLine(Player.Use(objStr1, objStr2));
                        }
                        break;
                        // Om användaren släpper ett objekt
                    case Action.Släpp:
                        Console.WriteLine(Player.Drop(objStr1));
                        break;
                        // Om användaren går i en rikting
                    case Action.Gå:
                        if (direction != null)
                        {
                            string result = Player.Go((Direction)direction, out bool isSuccess);

                            // Om rum ändras rensas skärmen och skriver ut vad spelaren ser
                            if (isSuccess)
                            {
                                Console.Clear();
                                Console.WriteLine(result);
                            }
                            else
                            {
                                Console.WriteLine(result);
                            }
                        }
                        break;
                        // Om användaren försöker ta ett objekt
                    case Action.Ta:

                        // Om spelaren vill ta ett objekt ur ett annat objekt...
                        if (preposition == Preposition.Från || preposition == Preposition.I)
                        {
                            Console.WriteLine(Player.Get(objStr1, objStr2));

                        }
                        // Om spelaren vill ta ett objekt i rummet
                        else
                        {
                            Console.WriteLine(Player.Get(objStr1));
                        }
                        break;
                        // Om användaren försöker prata med någon/något
                    case Action.Prata:
                        Console.WriteLine(Player.Talk(objStr1));
                        break;
                        // Om användaren vill ha hjälp med syntax
                    case Action.Hjälp:
                        Console.Clear();
                        Console.WriteLine("\n[GÅ]+[NORR|SYD|ÖST|VÄST]" +
                                          "\nGå i riktning.  " +
                                          "\nExempel: \"gå norr\"\n" +
                                          "\n[TA] + [<FÖREMÅL>]" +
                                          "\nTar upp ett föremål och lägger i fickan" +
                                          "\nExempel: \"ta bilnyckel\"\n" +
                                          "\n[SLÄPP] + [<FÖREMÅL>]" +
                                          "\nSläpper föremålet i rummet" +
                                          "\nExempel: \"släpp hammare\"\n" +
                                          "\n[TITTA] | [TITTA] + [PÅ|I] [<FÖREMÅL>]" +
                                          "\nTittar på uppplockat föremål, föremål i annat föremål eller på föremål i rummet" +
                                          "\nExempel: \"titta\" eller \"titta i ficka\" eller \"titta på bilnyckel\"\n" +
                                          "\n[INSPEKTERA] + [<FÖREMÅL>] | [INSPEKTERA] + [<FÖREMÅL>] + [I] + [<FÖREMÅL>]" +
                                          "\nGer en detaljerad beskrivning av ett föremål" +
                                          "\nExempel: \"inspektera hammare\" eller \"inspektera nyckel i ficka\"\n" +
                                          "\n[ANVÄND] + [<FÖREMÅL>] + [PÅ] + [<FÖREMÅL> | [<UTGÅNG>]" +
                                          "\nAnvänder föremål på annat föremål eller utgång" +
                                          "\nExempel: \"använd bilnyckel på bil\" eller \"använd nyckel på trädörr\"\n");
                        break;
                        // Om användaren skriver in "avsluta"
                    case Action.Avsluta:
                        if (!QuitGameDialog())
                        {
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return true;
        }

        // Lägger till objekt i rum, om objekt är av typen "exit" så läggs den till i rummets Exit-lista också
        private static void AddGameObjectsToRoom(Room room, GameObject gameObject)
        {
            if (gameObject is Exit)
            {
                room.Exits.Add(gameObject.Key, gameObject as Exit);
                room.Objects.Add(gameObject.Key, gameObject);
            }
            else
            {
                room.Objects.Add(gameObject.Key, gameObject);
            }
        }
        // Lägger till objekt i ett annat objekt.
        private static void AddGameObjectsToContainer(GameObjectsHolder container, GameObject gameObject)
        {
            container.Objects.Add(gameObject.Key, gameObject);
        }

        // Metod som kollar om strängen som matats in är enligt tillåten syntax
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

        // Kategoriserar användarens strängar efter spelets ordklasser. Returnerar null om inget ord av ordklassen finns med i meningen
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
                if (IsDirectionEnum(split[1]))
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

        // Kollar om strängen som skickas in är en Direction
        public static bool IsDirectionEnum(string str)
        {
            return Enum.TryParse(str, true, out Direction dir);
        }

        // Metod som frågar om användaren vill avsluta
        private static bool QuitGameDialog()
        {
            while (true)
            {
                Console.WriteLine("Vill du avsluta spelet?");
                Console.Write("J/N: ");
                string answer = Console.ReadLine();
                if (answer.ToUpper() == "N")
                {
                    Console.WriteLine("Okej, spelet fortsätter!");
                    Console.ReadLine();
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
            return true;
        }

        // Metod som frågar om användaren vill spela igen om spelet är avklarat eller spelaren dör
        private static bool RestartGameDialog()
        {
            while (true)
            {
                Console.WriteLine("Vill du prova igen?");
                Console.Write("J/N: ");
                string answer = Console.ReadLine();
                if (answer.ToUpper() == "J")
                {
                    Console.WriteLine("Okej, spelet börjar om!");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else if (answer.ToUpper() == "N")
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
            return true;
        }
    }
}
