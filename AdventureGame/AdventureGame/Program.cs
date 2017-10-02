using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventureGame
{
    class Program
    {
        
        static void Main(string[] args)
        {

            bool isRunning = true;

            // Sätter igång spelmenyn
            Game.GameMenu.DoMenu();
           
            // Loop som håller igång spelet till dess att spelaren väljer att avsluta i game.Play
            while (isRunning)
            {
                // Ny spelomgång skapas
                Game game = new Game();

                // En spelomgång i Play
                bool playAgain = game.Play();

                // Om spelaren valt att den inte vill spela igen så stängs spelet ner
                if (!playAgain)
                {
                    break;
                }
            }
        }
    }
}
