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
            while (isRunning)
            {
                //Game.GameMenu.DoMenu();
                Game game = new Game();
                Game.GameMenu.DoMenu();


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
