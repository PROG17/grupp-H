using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;
using AdventureGame.AdventureData.Interact;
using System.Text.RegularExpressions;

namespace AdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            while (true)
            {
                Console.WriteLine(game.Player.PlayerLocation.Description);
                Console.Write("Vad vill du göra? ");
                string input = Console.ReadLine();

                if (input.ToUpper().Contains("TITTA"))
                {
                    var split = input.Split(' ');
                    var objString = split[split.Length - 1];
                    AdventureData.GameObject obj1;
                    if (game.Player.PlayerLocation.Objects.TryGetValue(objString, out obj1))
                    {
                        Console.WriteLine(obj1.Description);
                    }
                    else if (Regex.IsMatch("NORR|SYD|ÖST|VÄST", objString))
                    {
                        var objectsInDriection = game.Player.PlayerLocation.Exits;
                    }
                    else if (objString == split[0])
                    {
                        Console.WriteLine(game.Player.PlayerLocation.Description);
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                else if (input.ToUpper().Contains("TA"))
                {
                    
                }
                else if (input.ToUpper().Contains("SLÄPP"))
                {
                    
                }
                else if (input.ToUpper().Contains("ANVÄND"))
                {

                }

                if (input.ToUpper() == "TITTA ÖST")
                {
                    Console.WriteLine($"Till öst ligger {game.Player.PlayerLocation.Objects["nyckel"].Name}");
                    Console.ReadLine();
                    Console.Clear();
                }
                if (input.ToUpper() == "TITTA NORR")
                {
                    Console.WriteLine($"Till norr ligger {game.Player.PlayerLocation.Exits[0].Name}");
                    Console.ReadLine();
                    Console.Clear();
                }
                if (input.ToUpper() == "TA NYCKEL")
                {
                    AdventureData.Interact.Actions.Get(game.Player, game.Player.PlayerLocation.Objects["nyckel"]);
                    Console.WriteLine("Du tog nyckeln");
                    Console.ReadLine();
                    Console.Clear();
                }
                if (input.ToUpper() == "ANVÄND NYCKEL PÅ DÖRR")
                {
                    if (game.Player.Objects.Count != 0)
                    {
                        Actions.Use(game.Player, game.Player.Objects["nyckel"], game.Player.PlayerLocation.Exits[0]);
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
                if (input.ToUpper() == "GÅ NORR")
                {
                    if (game.Player.PlayerLocation.Exits[0].IsLocked)
                    {
                        Console.WriteLine("Dörren är låst");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Actions.Go(game.Player, game.Player.PlayerLocation, Directions.North);
                    }
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
        }
    }
}
