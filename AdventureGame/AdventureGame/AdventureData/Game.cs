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
            Player = new Player
            {
                Name = "Andreas",
                Description = "Aktuell spelare",
            };
            Rooms = new Dictionary<string, Room>();

            var start = new Room
            {
                Name = "Källare",
                Description = "Du är i en mörk källare",
                Exits = new List<Exit>(),
                IsEndPoint = false,
            };
            var end = new Room
            {
                Name = "Baksidan av huset",
                Description = "Du står på baksidan av huset, luften är frisk",
                Exits = new List<Exit>(),
                IsEndPoint = true
            };

            var dorr = new Exit
            {
                Name = "Stor dörr",
                Description = "Stor trädörr",
                GoesTo = end,
                IsLocked = true,
                InDirection = Directions.North,

            };

            var nyckel = new Object
            {
                Name = "nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                Direction = Directions.East,
                ObjectTransformed = null
            };
            Player.PlayerLocation = start;

            start.Objects.Add("nyckel", nyckel);
            start.Objects.Add("dörr", dorr);
            start.Exits.Add(dorr);
            Rooms.Add("start", start);
            Rooms.Add("end", end);
        }
    }
}
