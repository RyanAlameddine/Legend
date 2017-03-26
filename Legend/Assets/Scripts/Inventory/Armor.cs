using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public abstract class Armor : Item {
    public int defence;

    public override void GenerateDescription()
    {
        this.description = name + " has " + defence + " defence.\nYou can sell it for " + cost + " coins.";
    }
}

[CustomEditor(typeof(Armor))]
public class ArmorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Armor i = (Armor)target;
        base.DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            i.GenerateDescription();
            Undo.RecordObject(i, "Generated Item");
            EditorUtility.SetDirty(i);
        }
    }
}
