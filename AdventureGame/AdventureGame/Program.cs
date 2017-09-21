using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;

namespace AdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.Write("Skriv ditt namn: ");
            game.Player.Name = Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"Välkommen {game.Player.Name}");
            Console.ReadLine();
        }
    }
}
