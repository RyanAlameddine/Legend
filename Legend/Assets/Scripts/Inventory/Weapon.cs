using UnityEditor;
using UnityEngine;
[System.Serializable]
public class Weapon : Item {
    public int damage;
    public WeaponPower power;

    public override void GenerateDescription()
    {
        description = "This " + name + " deals " + damage + " damage.\nIt has " + power + " powers.\nYou can sell it for " + cost + " coins.";
    }
}

[CustomEditor(typeof(Weapon))]
public class WeaponInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Weapon i = (Weapon)target;
        base.DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            i.GenerateDescription();
            Undo.RecordObject(i, "Generated Item");
            EditorUtility.SetDirty(i);
        }
    }
}