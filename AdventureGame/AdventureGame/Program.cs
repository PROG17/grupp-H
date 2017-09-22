using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                Console.WriteLine();
                string input = Console.ReadLine();

                if (input == "")
                {
                    continue;
                }

                var split = input.ToUpper().Split(new []{" ", "PÅ", "MED", "TILL"}, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length > 3)
                {
                    Console.WriteLine("Jag förstod inte vad du menade...");
                    continue;
                }

                string firstWord = "";
                string secondWord = "";
                string thirdWord = "";

                //detta går att göra om till en for loop tänker jag. 
                //När vi flyttar detta till egna metoder tycker jag vi gör detta
                if (split.Length == 1)
                {
                    firstWord = split[0].ToLower();
                }
                else if (split.Length == 2)
                {
                    firstWord = split[0].ToLower();
                    secondWord = split[1].ToLower();
                }
                else
                {
                    firstWord = split[0].ToLower();
                    secondWord = split[1].ToLower();
                    thirdWord = split[2].ToLower();
                }

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

                        string startString = "Du ser ";

                        if (Directions.TryParse(secondWord, true, out Directions lookDirection))
                        {
                            string writeOut =
                                currentRoom.TryFindObjectInDirection(currentRoom, lookDirection, out GameObject roomobj)
                                    ? roomobj.Description.ToLower()
                                    : "en vägg";
                            Console.WriteLine($"{startString}{writeOut}");
                            Console.ReadLine();
                        }
                        else if (currentRoom.Objects.ContainsKey(secondWord.ToUpper()))
                        {
                            Console.WriteLine(currentRoom.Objects[secondWord.ToUpper()].Description);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine(currentRoom.Description);
                            Console.ReadLine();
                        }

                        break;
                    case Action.Använd:

                        if (split.Length == 3)
                        {
                            bool hasOject1 = player.Objects.TryGetValue(secondWord, out GameObject obj1);
                            bool hasObject2 = currentRoom.Objects.TryGetValue(thirdWord, out GameObject obj2);
                            if (hasOject1 && hasObject2)
                            {

                                Act.Use(player, obj1, obj2);

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
                        }

                        break;
                    case Action.Släpp:
                        break;
                    case Action.Gå:

                        if (!Directions.TryParse(secondWord, true, out Directions walkDirection))
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
                        if (currentRoom.Objects.TryGetValue(secondWord, out GameObject takeObject))
                        {
                            Act.Get(player, takeObject);
                            Console.WriteLine($"Du tog {takeObject.Name}");
                            Console.ReadLine();
                        }
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

        public void Contents(Dictionary<string, GameObject> gameobjectdictDictionary)
        {
            Console.WriteLine("I rummet finns även");
            foreach (KeyValuePair<string, GameObject> t in gameobjectdictDictionary)
            {
                Console.WriteLine(gameobjectdictDictionary.Keys);
            }
        }
    }
}
