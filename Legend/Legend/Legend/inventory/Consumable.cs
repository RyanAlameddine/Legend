using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.inventory
{
    public class Consumable : Item
    {
        public int health;

        public Consumable(string name, Texture2D texture, int health, int cost)
            :base(texture)
        {
            this.name = name;
            this.health = health;
            this.cost = cost;
            this.description = name + " restore " + health + " health.\nYou can sell it for " + cost + " coins.";
            type = ItemType.Consumable;
        }

        public override void Update()
        {

        }

        public override string getDescription()
        {
            return description;
        }

        public override Item Copy()
        {
            return new Consumable(name, texture, health, cost);
        }
    }
}
