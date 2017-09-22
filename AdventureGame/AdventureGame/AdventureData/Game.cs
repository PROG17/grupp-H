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
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = false,
            };
            var end = new Room
            {
                Name = "Baksidan av huset",
                Description = "Du står på baksidan av huset, luften är frisk",
                Exits = new Dictionary<string, Exit>(),
                IsEndPoint = true
            };

            var dorr = new Exit
            {
                Name = "Stor dörr",
                Description = "Stor trädörr",
                GoesTo = end,
                IsLocked = true,
                InDirection = Directions.Norr,

            };

            var nyckel = new Object
            {
                Name = "nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                Direction = Directions.Öst,
                ObjectTransformed = null
            };
            Player.PlayerLocation = start;

            start.Objects.Add(nyckel.Name.ToUpper(), nyckel);
            start.Objects.Add(dorr.Name.ToUpper(), dorr);
            start.Exits.Add(dorr.Name.ToUpper(),dorr);
            Rooms.Add("start", start);
            Rooms.Add("end", end);
        }
    }
}
