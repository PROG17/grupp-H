﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Permissions;
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

            //SoundPlayer Music = new SoundPlayer();
            //Music.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BeepBox-Song.wav";
            //Music.Play();

            bool isPlaying = true;
            while (isPlaying)
            {
                isPlaying = game.Prompt();
            }
        }
    }
}
