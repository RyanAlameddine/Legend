using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Item : ScriptableObject
{
    //public ItemType type;
    [HideInInspector]
    public bool equiptstatus = false;
    [HideInInspector]
    public string description = "";
    public new string name = "";
    public int cost = 0;
    public Sprite sprite;

    public void togglequpited()
    {
        equiptstatus = !equiptstatus;
    }

    public virtual void GenerateDescription()
    {

    }
}

[CustomEditor(typeof(Item))]
public class ItemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Item i = (Item)target;
        base.DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            i.GenerateDescription();
            Undo.RecordObject(i, "Generated Item");
            EditorUtility.SetDirty(i);
        }
    }
}
