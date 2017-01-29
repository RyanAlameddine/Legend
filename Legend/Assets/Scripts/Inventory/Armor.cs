using UnityEngine;
using System.Collections;
[System.Serializable]
public class Armor : Item {
    public int defence;

    public Armor(string name, int defence, int cost, Sprite sprite)
        {
        this.name = name;
        this.defence = defence;
        this.cost = cost;
        this.description = name + " has " + defence + " defence.\nYou can sell it for " + cost + " coins.";
        type = ItemType.Armour;
        this.sprite = sprite;
    }
}
