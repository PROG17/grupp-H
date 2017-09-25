﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class GameObjectsHolder : GameObject
    {
        public virtual Dictionary<string, GameObject> Objects { get; set; }

        public virtual string GetContentAsString()
        {
            if (Objects.Count != 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var gameObject in Objects)
                {
                    sb.AppendLine($"{gameObject.Value.Name}");
                }

                return sb.ToString();
            }
            else return null;
        }

        public GameObjectsHolder()
        {
            Objects = new Dictionary<string, GameObject>();
        }

        public bool DropFirstItem(Room currentRoom)
        {
            Stack<GameObject> listOfObjects = new Stack<GameObject>();
            if (Objects.Count != 0)
            {
                foreach (var gameObject in Objects)
                {
                    listOfObjects.Push(gameObject.Value);
                }
                GameObject obj = listOfObjects.Pop();
                currentRoom.Objects.Add(obj.Key, obj);
                this.Objects.Remove(obj.Key);
                return true;
            }
            return false;
        }
    }
}
