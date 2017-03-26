using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Misc : Item {
    public override void GenerateDescription()
    {
        this.description = description + "\nYou can sell it for " + cost + " coins.";
    }
}

[CustomEditor(typeof(Misc))]
public class MiscInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Misc i = (Misc)target;
        base.DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            i.GenerateDescription();
            Undo.RecordObject(i, "Generated Item");
            EditorUtility.SetDirty(i);
        }
    }
}
