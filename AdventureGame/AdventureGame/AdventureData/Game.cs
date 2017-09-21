using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class Game
    {

        public Dictionary<string, Room> Rooms { get; set; }
        public Player Player { get; set; }

        public Game()
        {
            Player = new Player();
            Rooms = new Dictionary<string, Room>();

            var start = new Room
            {
                Name = "Källare",
                Description = "Du är i en mörk källare"
            };

            var nyckel = new Object
            {
                Name = "Nyckel",
                Description = "En stor rostig nyckel"
            };

            start.Objects.Add("nyckel", nyckel);
            Rooms.Add("start", start);
        }
    }
}
