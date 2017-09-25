using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class GameObjectsHolder : GameObject
    {
        public virtual Dictionary<string, GameObject> Objects { get; set; }

        public virtual string GetDescriptionWithContent()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(this.Description);

            foreach (var gameObject in Objects)
            {
                sb.AppendLine($"{gameObject.Value.Name}");
            }

            return sb.ToString();
        }

        public GameObjectsHolder()
        {
            Objects = new Dictionary<string, GameObject>();
        }
    }
}
