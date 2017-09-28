using System;
using System.Collections.Generic;

namespace AdventureGame.AdventureData
{
    public class Person : GameObjectsHolder
    {
        // Dialog innehåller det personen säger om player pratar med den
        public string Dialog { get; set; }

        // Konstruktor
        public Person()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}