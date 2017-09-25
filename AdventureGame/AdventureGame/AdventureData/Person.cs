using System;
using System.Collections.Generic;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    public class Person : GameObjectsHolder
    {
        public string Dialog { get; set; }
        public bool HitsBack { get; set; }
        public Person()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}