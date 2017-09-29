using System;
using System.Collections.Generic;

namespace AdventureGame.AdventureData
{
    public class Person : GameObjectsHolder
    {
        // Dialog innehåller det personen säger om player pratar med den
        public string Dialog { get; set; }

        // Objekt som transformerar person till glad eller arg
        public Object GetsHappyWith { get; set; }
        public Object GetsAngryWith { get; set; }
        // Person som ursprungliga person transformeras till
        public Person TransformsToHappy { get; set; }
        public Person TransformsToAngry { get; set; }
        // Objekt som person släpper/tappar beroende på vilket objekt som släpps
        public Object DropsIfHappyObject { get; set; }
        public Object DropsIfAngryObject { get; set; }

        public Object TranformToHappy(Room personLocation)
        {
            personLocation.Objects.Remove(this.Key);
            personLocation.Objects.Add(TransformsToHappy.Key, TransformsToHappy);
            return DropsIfHappyObject;
        }
        public Object TranformToAngry(Room personLocation)
        {
            personLocation.Objects.Remove(this.Key);
            personLocation.Objects.Add(TransformsToAngry.Key, TransformsToAngry);
            return DropsIfAngryObject;
        }

        // Konstruktor
        public Person()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}