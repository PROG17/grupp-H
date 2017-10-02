using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.AdventureData
{
    public class GameObject
    {
        // Namn
        public string Name { get; set; }
        // Beskrvning
        public string Description { get; set; }
        // Property för Nullbar Direction-variabel
        public Direction? DirectionalPosition { get; set; }
        // Property för Lista av strängar som representerar objekt som detta
        // objekt kan användas med
        public List<string> CanUseWith { get; set; }
        // Property för GameObject som är det objekt detta objekt omvandlas till
        // när det används med annat giltigt objekt
        public virtual GameObject ObjectTransformed { get; set; }
        // Bool för att kolla om objektet går att plocka upp
        public bool IsGetable { get; set; } = false;
        // Bool för att kolla om objektet "tappar" objekt när giltigt objekt
        // används på den
        public bool DropsItemOnUse { get; set; } = false;
        // Lista med giltiga objekt som dödar Player
        public List<GameObject> KillsOnUse { get; set; }

        public GameObject()
        {
            CanUseWith = new List<string>();
            KillsOnUse = new List<GameObject>();
        }

        // Property för att skapa en nyckel för objektet i dictionaries
        public string Key
        {
            get
            {
                var split = Name.Split(' ');
                return split[split.Length - 1].ToLower();
            }
        }
    }
}
