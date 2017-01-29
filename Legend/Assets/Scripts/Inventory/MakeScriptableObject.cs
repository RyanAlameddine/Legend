using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/Item")]
    public static void CreateMyAsset()
    {
        ItemStats asset = ScriptableObject.CreateInstance<ItemStats>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}