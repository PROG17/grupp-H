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
            var trasigTunna = new GameObjectContainer
            {
                Name = "Spillrorna efter en stor tunna",
                Description = "Trasig tunna, träflisor och gjutjärnsringar. Kanske ligger det något bland spillrorna",
                DirectionalPosition =  Directions.syd,
                ObjectTransformed = null
            };
            var tunna = new GameObjectContainer
            {
                Name = "tunna",
                Description = "Stor tunna i trä och gjutjärn. Tunnan går inte att öppna utan något verktyg...",
                CanUseWith = "HAMMARE",
                ObjectTransformed = trasigTunna,
                DirectionalPosition = Directions.syd
                
                
            };
            
            

            var dorr = new Exit
            {
                Name = "dörr",
                Description = "Stor trädörr",
                GoesTo = end,
                IsLocked = true,
                DirectionalPosition = Directions.Norr,

            };

            var nyckel = new Object
            {
                Name = "nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                DirectionalPosition = Directions.syd,
                ObjectTransformed = null
            };

            var hammer = new Object
            {
                Name = "hammare",
                Description = "en robust hammare",
                CanUseWith = tunna.Name,
                DirectionalPosition = Directions.väst,
                ObjectTransformed = null
            };

            Player.PlayerLocation = start;
            tunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Objects.Add(dorr.Name.ToLower(), dorr);
            start.Objects.Add(tunna.Name.ToLower(), tunna);
            start.Objects.Add(hammer.Name.ToLower(), hammer);
            //start.Objects.Add(nyckel.Name.ToLower(), nyckel);
            //tunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Exits.Add(dorr.Name.ToLower(), dorr);
            Rooms.Add("start", start);
            Rooms.Add("end", end);
            
        }
    }
}
