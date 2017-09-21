using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    class Exit : GameObject
    {
        public bool IsLocked { get; set; }
        public string GoesTo { get; set; }
        public string OpensWith { get; set; }
    }
}
