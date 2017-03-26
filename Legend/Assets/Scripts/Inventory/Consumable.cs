using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Consumable : Item {
    public int health;

    public override void GenerateDescription()
    {
        this.description = name + " restores " + health + " health.\nYou can sell it for " + cost + " coins.";
    }
}

[CustomEditor(typeof(Consumable))]
public class ConsumableInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Consumable i = (Consumable)target;
        base.DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            i.GenerateDescription();
            Undo.RecordObject(i, "Generated Item");
            EditorUtility.SetDirty(i);
        }
    }
}
