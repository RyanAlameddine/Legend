using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.inventory
{
    public class Armour : Item
    {
        int defence;
        string description;
        int cost;
        

        public Armour(string name, Texture2D texture, int defence, int cost)
            :base(texture)
        {
            this.name = name;
            this.defence = defence;
            this.cost = cost;
            this.description = name + " has " + defence + " defence.\nYou can sell it for " + cost + " coins.\nIt is " + equiptstatus;
            type = ItemType.Armour;
        }

        public override void Update()
        {
            this.description = name + " has " + defence + " defence.\nYou can sell it for " + cost + " coins.\nIt is " + equiptstatus;
        }

        public override string getDescription()
        {
            return description;
        }
    }
}
