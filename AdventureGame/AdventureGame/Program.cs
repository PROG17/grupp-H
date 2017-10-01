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
            Game.GameMenu.DoMenu();
           
            while (isRunning)
            {
                
                Game game = new Game();
               


                bool playAgain = game.Prompt();
                if (playAgain)
                {
                    game = new Game();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
