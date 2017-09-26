using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData;

namespace AdventureGame
{
    interface IInteractable
    {
        string Get(string obj);
        string Get(string toGet, string getFrom);
        string Drop(string obj);
        string Use(string objUseStr, string objUseOnStr);
        string Look();
        string LookIn(string obj);
        string LookAt(string obj);
        string LookTo(Direction direction);
        string Inspect(string obj);
        string InspectIn(string obj, string objContainer);
        string Go(Direction direction);
        string Talk(string obj);
    }
}
