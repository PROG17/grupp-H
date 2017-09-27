﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.AdventureData.Interact;

namespace AdventureGame.AdventureData
{
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Direction? DirectionalPosition { get; set; }
        public List<string> CanUseWith { get; set; }
        public virtual GameObject ObjectTransformed { get; set; }
        public bool IsGetable { get; set; } = false;
        public bool DropsItemOnUse { get; set; } = false;
        public List<GameObject> KillsOnUse { get; set; }

        public GameObject()
        {
            CanUseWith = new List<string>();
            KillsOnUse = new List<GameObject>();
        }

        public bool CanBeUsedWith(string obj)
        {
            return CanUseWith.Contains(obj);
        }

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
