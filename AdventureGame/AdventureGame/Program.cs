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

            Console.WriteLine(currentRoom.Description);
            while (true)
            {
                Console.Write("Vad vill du göra? ");
                Console.WriteLine();
                string input = Console.ReadLine();

                var split = input.ToLower().Split(' ');

                if (!Game.ValidateSentence(split, out string[] sentence))
                {
                    Console.WriteLine("Jag förstod inte vad du menade...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                string firstWord = "";
                string secondWord = "";
                string thirdWord = "";

                string actionStr = "";
                string preposString = "";
                string directionStr = "";
                string obj_1 = "";
                string obj_2 = "";

                //if (split.Length == 1)
                //{
                //    firstWord = split[0].ToLower();
                //}
                //else if (split.Length == 2)
                //{
                //    firstWord = split[0].ToLower();
                //    secondWord = split[1].ToLower();
                //}
                //else
                //{
                //    firstWord = split[0].ToLower();
                //    secondWord = split[1].ToLower();
                //    thirdWord = split[2].ToLower();
                //}
                if (split.Length == 1)
                {
                    actionStr = split[0];
                }
                else if (split.Length == 2)
                {
                    actionStr = split[0];

                    if (Act.IsDirectionEnum(split[1]))
                    {
                        directionStr = split[1];
                    }
                    else
                    {
                        obj_1 = split[1];
                    }
                    

                }
                else if (split.Length == 3)
                {
                    actionStr = split[0];
                    preposString = split[1];
                    obj_1 = split[2];
                }
                else if (split.Length == 4)
                {
                    actionStr = split[0];
                    obj_1 = split[1];
                    preposString = split[2];
                    obj_2 = split[3];
                }


                switch (Enum.Parse(typeof(Action), actionStr, true))
                {
                    case Action.Titta:

                        string startString = "Du ser ";
                        if (split.Length == 2)
                        {
                            if (Direction.TryParse(directionStr, true, out Direction lookDirection))
                            {
                                string writeOut =
                                    currentRoom.TryFindObjectInDirection(currentRoom, lookDirection, out GameObject roomobj)
                                        ? roomobj.Description.ToLower()
                                        : "en vägg";
                                Console.WriteLine($"{startString}{writeOut}");
                                Console.ReadLine();
                            }
                        }
                        else if (split.Length == 3)
                        {

                            if (currentRoom.Objects.ContainsKey(obj_1))
                            {
                                Console.WriteLine(
                                    Act.Look(currentRoom.Objects[obj_1],(Preposition)Enum.Parse(typeof(Preposition), preposString, true)));
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
                            bool hasOject1 = player.Objects.TryGetValue(obj_1, out GameObject obj1);
                            bool hasObject2 = currentRoom.Objects.TryGetValue(obj_2, out GameObject obj2);
                            if (hasOject1 && hasObject2)
                            {

                                if (Act.Use(player, obj1, obj2))
                                {
                                    Console.WriteLine("Det gick!");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Det gick inte...");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                string noObj = "Det fanns ingen ";
                                noObj += hasOject1 ? obj_1 : "";
                                noObj += hasOject1 && hasObject2 ? " eller " + obj_2 : "";
                                noObj += hasObject2 ? obj_2 : "";
                            }
                        }

                        break;
                    case Action.Släpp:
                        break;
                    case Action.Gå:

                        if (!Direction.TryParse(secondWord, true, out Direction walkDirection))
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
                                Act.Go(game.Player, game.Player.PlayerLocation, Direction.Norr);
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
                        if (currentRoom.Objects.TryGetValue(obj_1, out GameObject takeObject))
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
                //        Direction.TryParse(firstWord, true, out Direction direction);
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
                //        Act.Go(game.Player, game.Player.PlayerLocation, Direction.North);
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
