using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class Exit : GameObject
    {
        // Innehåller rummet som denna Exit leder till
        public Room GoesTo { get; set; }
        // Innehåller det objekt som öppnar dörren
        public Object OpensWith { get; set; }
        // Bool för att kolla om dörren är öppen eller låst
        public bool IsLocked { get; set; }
        

        public Exit()
        {
            // Gör så dörrar inte går att plocka upp
            IsGetable = false;
        }

        // Metod som byter aktuellt rum när spelaren går igenom en exit.
        public void GoThrough(Player player)
        {
            var temp = GoesTo;
            player.PlayerLocation = null;
            player.PlayerLocation = GoesTo;
        }
    }
}
