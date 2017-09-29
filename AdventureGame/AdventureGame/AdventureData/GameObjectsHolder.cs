using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class GameObjectsHolder : GameObject
    {
        // Hållare av GameObjects
        public virtual Dictionary<string, GameObject> Objects { get; set; }

        // Skriver ut innehållet i this.Objects
        // Är virtual då andra klasser skriver om den en aning
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
            else return "Den är tom...";
        }

        public GameObjectsHolder()
        {
            Objects = new Dictionary<string, GameObject>();
        }

        // Metod som släpper första objektet i listan
        public bool DropFirstItem(Room currentRoom)
        {
            Queue<GameObject> listOfObjects = new Queue<GameObject>();
            if (Objects.Count != 0)
            {
                foreach (var gameObject in Objects)
                {
                    listOfObjects.Enqueue(gameObject.Value);
                }
                GameObject obj = listOfObjects.Dequeue();
                currentRoom.Objects.Add(obj.Key, obj);
                this.Objects.Remove(obj.Key);
                return true;
            }
            return false;
        }
    }
}
