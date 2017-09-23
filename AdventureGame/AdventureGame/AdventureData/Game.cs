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
                Name = "trasig tunna",
                Description = "en trasig tunna, träflisor och gjutjärnsringar. Kanske ligger det något bland spillrorna",
                DirectionalPosition = Direction.Syd,
                ObjectTransformed = null
            };
            var tunna = new GameObjectContainer
            {
                Name = "tunna",
                Description = "en stor tunna i trä och gjutjärn. Tunnan går inte att öppna utan något verktyg...",
                CanUseWith = "HAMMARE",
                ObjectTransformed = trasigTunna,
                DirectionalPosition = Direction.Syd


            };



            var dorr = new Exit
            {
                Name = "trädörr",
                Description = "en stor trädörr",
                GoesTo = end,
                IsLocked = true,
                DirectionalPosition = Direction.Norr,

            };

            var nyckel = new Object
            {
                Name = "nyckel",
                Description = "En stor rostig nyckel",
                CanUseWith = dorr.Name,
                DirectionalPosition = Direction.Syd,
                ObjectTransformed = null
            };

            var hammer = new Object
            {
                Name = "hammare",
                Description = "en robust hammare",
                CanUseWith = tunna.Name,
                DirectionalPosition = Direction.Väst,
                ObjectTransformed = null
            };

            Player.PlayerLocation = start;
            trasigTunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Objects.Add(dorr.Name.ToLower(), dorr);
            start.Objects.Add(tunna.Name.ToLower(), tunna);
            start.Objects.Add(hammer.Name.ToLower(), hammer);
            //start.Objects.Add(nyckel.Name.ToLower(), nyckel);
            //tunna.Objects.Add(nyckel.Name.ToLower(), nyckel);
            start.Exits.Add(dorr.Name.ToLower(), dorr);
            Rooms.Add("start", start);
            Rooms.Add("end", end);

        }

        public static bool ValidateSentence(string[] split, out string[] strings)
        {
            if (split.Length > 0 && split.Length < 4)
            {
                if (split.Length == 1)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        strings = split;
                        return true;
                    }
                }
                else if (split.Length == 2)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (action == Action.Gå)
                        {
                            if (Direction.TryParse(split[1], true, out Direction dir))
                            {
                                strings = split;
                                return true;
                            }
                        }
                        else if (action == Action.Släpp || action == Action.Ta)
                        {
                            strings = split;
                            return true;
                        }

                    }
                }
                else if (split.Length == 3)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (Preposition.TryParse(split[1], true, out Preposition prepos))
                        {
                            strings = split;
                            return true;
                        }
                    }
                }
                else if (split.Length == 4)
                {
                    if (Action.TryParse(split[0], true, out Action action))
                    {
                        if (Preposition.TryParse(split[2], true, out Preposition prepos))
                        {
                            strings = split;
                            return true;
                        }
                    }
                }
            }
            strings = null;
            return false;
        }
    }
}
