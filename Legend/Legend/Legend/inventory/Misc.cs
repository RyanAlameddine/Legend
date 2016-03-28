using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.inventory
{
    public class Misc : Item
    {
        string _description;
        public Misc(string name, Texture2D texture, int cost, string _description)
            :base(texture)
        {
            this.name = name;
            this.cost = cost;
            this.description = "This " + name + " is " + _description + ".\nYou can sell it for " + cost + " coins.";
            type = ItemType.Misc;
            this._description = _description;
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
            return new Misc(name, texture, cost, _description);
        }
    }
}
