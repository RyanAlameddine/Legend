using UnityEngine;
using System.Collections;
using UnityEditor;

public class ItemStats : MonoBehaviour {
    public Item itemAttributes;
    public ItemType type;
    public Item item;
    //Armor
    public int defence;
    //Weapon
    public int damage;
    public WeaponPower power;
    public int health;
}


[CustomEditor(typeof(ItemStats))]
public class StatsEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as ItemStats;

        myScript.type = (ItemType) EditorGUILayout.EnumPopup("Item Type:", myScript.type);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cost:");
        myScript.itemAttributes.cost = EditorGUILayout.IntSlider(myScript.itemAttributes.cost, 0, 1000);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Equipped:");
        myScript.itemAttributes.equiptstatus = GUILayout.Toggle(myScript.itemAttributes.equiptstatus, "");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Item Name:");
        myScript.itemAttributes.name = EditorGUILayout.TextField("");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Image:");
        myScript.itemAttributes.sprite = (Sprite)EditorGUILayout.ObjectField(myScript.itemAttributes.sprite, typeof(Sprite), true);
        EditorGUILayout.EndHorizontal();
        myScript.itemAttributes.type = myScript.type;
        if (myScript.type == ItemType.Armour)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Defence:");
            myScript.defence = EditorGUILayout.IntSlider(myScript.defence, 0, 100);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Generate"))
            {
                myScript.item = new Armor(myScript.itemAttributes.name, myScript.defence, myScript.itemAttributes.cost, myScript.itemAttributes.sprite);
            }
        }
        else if (myScript.type == ItemType.Weapon)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Damage:");
            myScript.damage = EditorGUILayout.IntSlider(myScript.damage, 0, 100);
            EditorGUILayout.EndHorizontal();
            myScript.power = (WeaponPower) EditorGUILayout.EnumPopup("Weapon Power:", myScript.power);

            if (GUILayout.Button("Generate"))
            {
                myScript.item = new Weapon(myScript.itemAttributes.name, myScript.damage, myScript.power, myScript.itemAttributes.cost, myScript.itemAttributes.sprite);
            }
        }
        else if (myScript.type == ItemType.Misc)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Description:");
            myScript.itemAttributes.description = EditorGUILayout.TextField("");
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Generate"))
            {
                myScript.item = new Misc(myScript.itemAttributes.name, myScript.itemAttributes.cost, myScript.itemAttributes.description, myScript.itemAttributes.sprite);
            }
        }
        else if (myScript.type == ItemType.Consumable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Health:");
            myScript.health = EditorGUILayout.IntSlider(myScript.health, 0, 100);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Generate"))
            {
                myScript.item = new Consumable(myScript.itemAttributes.name, myScript.itemAttributes.sprite, myScript.health, myScript.itemAttributes.cost);
            }
        }
        
    }
}