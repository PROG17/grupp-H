using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;
using AdventureGame.AdventureData.Interact;
using System.Text.RegularExpressions;
using Action = AdventureGame.AdventureData.Action;

namespace AdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            while (true)
            {
                Console.WriteLine(game.Player.PlayerLocation.Description);
                Console.Write("Vad vill du göra? ");
                string input = Console.ReadLine();

                var split = input.Split(' ');
                var firstWord = split[0];
                var lastWord = split[split.Length - 1];

                if (!Action.TryParse(firstWord, true, out Action action))
                {
                    Console.WriteLine("Jag förstod inte vad du menade...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                switch (action)
                {
                    case Action.Titta:

                        if (Directions.TryParse(lastWord, true, out Directions lookDirection))
                        {
                            string writeOut =
                                currentRoom.TryFindExitFromDirection(currentRoom, lookDirection, out Exit exit)
                                    ? exit.Description.ToLower()
                                    : "en vägg";
                            Console.WriteLine($"Du ser {writeOut}");
                            Console.ReadLine();
                        }
                        else if (currentRoom.Objects.ContainsKey(lastWord.ToUpper()))
                        {
                            Console.WriteLine(currentRoom.Objects[lastWord.ToUpper()].Description);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine(currentRoom.Description);
                            Console.ReadLine();
                        }

                        break;
                    case Action.Använd:
                        if (player.Objects.TryGetValue(lastWord, out GameObject obj))
                        {
                            
                            //Act.Use(game.Player, game.Player.Objects["nyckel"], );
                            Console.WriteLine("Dörren är nu olåst");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Du har ingen nyckel!");
                            Console.ReadLine();
                            Console.Clear();
                        }

                        break;
                    case Action.Släpp:
                        break;
                    case Action.Gå:

                        if (!Directions.TryParse(lastWord, true, out Directions walkDirection))
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
                                Act.Go(game.Player, game.Player.PlayerLocation, Directions.Norr);
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
                        //    AdventureData.Interact.Act.Get(game.Player, game.Player.PlayerLocation.Objects["nyckel"]);
                        //    Console.WriteLine("Du tog nyckeln");
                        //    Console.ReadLine();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //if (input.ToUpper().Contains("TITTA"))
                //{
                    
                //    AdventureData.GameObject obj1;
                //    if (game.Player.PlayerLocation.Objects.TryGetValue(firstWord, out obj1))
                //    {
                //        Console.WriteLine(obj1.Description);
                //    }
                //    else if (Regex.IsMatch("NORR|SYD|ÖST|VÄST", firstWord))
                //    {
                //        Directions.TryParse(firstWord, true, out Directions direction);
                //        if (currentRoom.TryFindExitFromDirection(currentRoom, direction, out Exit exit))
                //        {
                //            Console.WriteLine($"Till {firstWord.ToLower()} ser du {exit.Description}");
                //            Console.ReadLine();
                //        }
                //    }
                //    else if (firstWord == split[0])
                //    {
                //        Console.WriteLine(game.Player.PlayerLocation.Description);
                //        Console.ReadLine();
                //        Console.Clear();
                //    }
                //}
                //else if (input.ToUpper().Contains("TA"))
                //{
                    
                //}
                //else if (input.ToUpper().Contains("SLÄPP"))
                //{
                    
                //}
                //else if (input.ToUpper().Contains("ANVÄND"))
                //{

                //}

                //if (input.ToUpper() == "TITTA ÖST")
                //{
                //    Console.WriteLine($"Till öst ligger {game.Player.PlayerLocation.Objects["nyckel"].Name}");
                //    Console.ReadLine();
                //    Console.Clear();
                //}
                //if (input.ToUpper() == "TITTA NORR")
                //{
                //    Console.WriteLine($"Till norr ligger {game.Player.PlayerLocation.Exits[0].Name}");
                //    Console.ReadLine();
                //    Console.Clear();
                //}
                //if (input.ToUpper() == "TA NYCKEL")
                //{
                //    AdventureData.Interact.Act.Get(game.Player, game.Player.PlayerLocation.Objects["nyckel"]);
                //    Console.WriteLine("Du tog nyckeln");
                //    Console.ReadLine();
                //    Console.Clear();
                //}
                //if (input.ToUpper() == "ANVÄND NYCKEL PÅ DÖRR")
                //{
                //    if (game.Player.Objects.Count != 0)
                //    {
                //        currentRoom.TryFindExitFromDirection()
                //        Act.Use(game.Player, game.Player.Objects["nyckel"], );
                //        Console.WriteLine("Dörren är nu olåst");
                //        Console.ReadLine();
                //        Console.Clear();
                //    }
                //    else
                //    {
                //        Console.WriteLine("Du har ingen nyckel!");
                //        Console.ReadLine();
                //        Console.Clear();
                //    }
                //}
                //if (input.ToUpper() == "GÅ NORR")
                //{
                //    if (game.Player.PlayerLocation.Exits[0].IsLocked)
                //    {
                //        Console.WriteLine("Dörren är låst");
                //        Console.ReadLine();
                //        Console.Clear();
                //    }
                //    else
                //    {
                //        Act.Go(game.Player, game.Player.PlayerLocation, Directions.North);
                //    }
                //}
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
        }
    }
}
