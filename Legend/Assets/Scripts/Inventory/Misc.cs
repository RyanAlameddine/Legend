using UnityEngine;
using System.Collections;
[System.Serializable]
public class Misc : Item {
    public Misc(string name, int cost, string description, Sprite sprite)
    {
        this.name = name;
        this.cost = cost;
        this.description = description + "\nYou can sell it for " + cost + " coins.";
        type = ItemType.Misc;
        this.sprite = sprite;
    }
}
