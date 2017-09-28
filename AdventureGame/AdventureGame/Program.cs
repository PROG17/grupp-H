using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;
using System.Text.RegularExpressions;
using Action = AdventureGame.AdventureData.Action;
using System.Threading;

namespace AdventureGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            GameMenu.DoMenu();
            Game game = new Game();

            Player player = Game.Player;
            Room currentRoom = player.PlayerLocation;

            SoundPlayer music = new SoundPlayer
            {
                SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BeepBox-Song.wav"
            };
            music.PlayLooping();

            bool isPlaying = true;
            while (isPlaying)
            {
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
