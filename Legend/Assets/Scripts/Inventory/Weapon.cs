using UnityEngine;
[System.Serializable]
public class Weapon : Item {
    public int damage;
    public WeaponPower power;

    public Weapon(string name, int damage, WeaponPower power, int cost, Sprite sprite)
    {
        this.name = name;
        this.power = power;
        this.damage = damage;
        this.cost = cost;
        this.description = "This " + name + " deals " + damage + " damage.\nIt has " + power + " powers.\nYou can sell it for " + cost + " coins.";
        type = ItemType.Weapon;
        this.sprite = sprite;
    }
}
