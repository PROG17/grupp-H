using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Object : GameObject
    {
        public string CanUseWith { get; set; }
        public Object ObjectTransformed { get; set; }
        
    }
}
