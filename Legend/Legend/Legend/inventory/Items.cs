using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legend.inventory
{
    public static class Items
    {
        private static Dictionary<String, Item> items = new Dictionary<string, Item>()
        {
            { "Foam Sword", new Weapon("Foam Sword", GameContent.foamsword, 1, WeaponPower.no, 1) },
            { "T-Shirt Armour", new Armour("T-Shirt Armour", GameContent.tshirt, 1, 1) },
            { "Gold Nugget", new Misc("Gold Nugget", GameContent.gold, 5, "A shiny gold nugget!") }
        };


        public static Item GetItem(string name)
        {
            return items[name].Copy();
        }
    }
}
