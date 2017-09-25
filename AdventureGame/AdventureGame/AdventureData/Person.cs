using System.Collections.Generic;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    public class Person:GameObjectsHolder
    {
        public Person()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}