using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend.inventory
{
    public class Weapon : Item
    {
        int damage;
        string description;
        WeaponPower power;
        int cost;


        public Weapon(string name, Texture2D texture, int damage, WeaponPower power, int cost)
            :base(texture)
        {
            this.name = name;
            this.power = power;
            this.damage = damage;
            this.cost = cost;
            this.description = "This " + name + " deals " + damage + " damage.\nIt has " + power + " powers.\nYou can sell it for " + cost + " coins.\nIt is " + equiptstatus;
            type = ItemType.Weapon;
        }

        public override void Update()
        {
            this.description = "This " + name + " deals " + damage + " damage.\nIt has " + power + " powers.\nYou can sell it for " + cost + " coins.\nIt is " + equiptstatus;
        }

        public override string getDescription()
        {
            return description;
        }
    }
}
