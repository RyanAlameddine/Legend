using UnityEngine;
using System.Collections;

[System.Serializable]
public class Consumable : Item {
    public int health;

    public Consumable(string name, Sprite sprite, int health, int cost)
    {
        this.name = name;
        this.health = health;
        this.cost = cost;
        this.description = name + " restore " + health + " health.\nYou can sell it for " + cost + " coins.";
        type = ItemType.Consumable;
        this.sprite = sprite;
    }
}
