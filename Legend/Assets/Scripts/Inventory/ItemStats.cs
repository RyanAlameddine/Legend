using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
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

    void Start()
    {
        Debug.Log(damage);
    }
}


[CustomEditor(typeof(ItemStats))]
public class StatsEditor : Editor
{

    override public void OnInspectorGUI()
    {
        ItemStats stats = (ItemStats) target;

        stats.type = (ItemType) EditorGUILayout.EnumPopup("Item Type:", stats.type);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cost:");
        stats.itemAttributes.cost = EditorGUILayout.IntSlider(stats.itemAttributes.cost, 0, 1000);
        EditorGUILayout.EndHorizontal();
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.PrefixLabel("Equipped:");
        //stats.itemAttributes.equiptstatus = GUILayout.Toggle(stats.itemAttributes.equiptstatus, "");
        //EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Item Name:");
        stats.itemAttributes.name = EditorGUILayout.TextField(stats.itemAttributes.name);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Image Name:");
        stats.itemAttributes.spriteName = EditorGUILayout.TextField(stats.itemAttributes.spriteName);
        EditorGUILayout.EndHorizontal();
        stats.itemAttributes.type = stats.type;
        if (stats.type == ItemType.Armour)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Defence:");
            stats.defence = EditorGUILayout.IntSlider(stats.defence, 0, 100);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Generate"))
            {
                stats.item = new Armor(stats.itemAttributes.name, stats.defence, stats.itemAttributes.cost, stats.itemAttributes.spriteName);
                Undo.RecordObject(stats, "Changed ItemStats");
                EditorUtility.SetDirty(stats);
            }
        }
        else if (stats.type == ItemType.Weapon)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Damage:");
            stats.damage = EditorGUILayout.IntSlider(stats.damage, 0, 100);
            EditorGUILayout.EndHorizontal();
            stats.power = (WeaponPower) EditorGUILayout.EnumPopup("Weapon Power:", stats.power);

            if (GUILayout.Button("Generate"))
            {
                stats.item = new Weapon(stats.itemAttributes.name, stats.damage, stats.power, stats.itemAttributes.cost, stats.itemAttributes.spriteName);
                Undo.RecordObject(stats, "Changed ItemStats");
                EditorUtility.SetDirty(stats);
            }
        }
        else if (stats.type == ItemType.Misc)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Description:");
            stats.itemAttributes.description = EditorGUILayout.TextField("");
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Generate"))
            {
                stats.item = new Misc(stats.itemAttributes.name, stats.itemAttributes.cost, stats.itemAttributes.description, stats.itemAttributes.spriteName);
                Undo.RecordObject(stats, "Changed ItemStats");
                EditorUtility.SetDirty(stats);
            }
        }
        else if (stats.type == ItemType.Consumable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Health:");
            stats.health = EditorGUILayout.IntSlider(stats.health, 0, 100);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Generate"))
            {
                stats.item = new Consumable(stats.itemAttributes.name, stats.health, stats.itemAttributes.cost, stats.itemAttributes.spriteName);
                Undo.RecordObject(stats, "Changed ItemStats");
                EditorUtility.SetDirty(stats);
            }
        }
        Undo.RecordObject(stats, "Changed ItemStats");
        EditorUtility.SetDirty(stats);
    }
}