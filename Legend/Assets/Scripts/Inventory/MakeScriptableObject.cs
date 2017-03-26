using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    //[MenuItem("Assets/Create/ItemStats")]
    //public static void CreateItemStats()
    //{
    //    ItemStats asset = ScriptableObject.CreateInstance<ItemStats>();

    //    AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
    //    AssetDatabase.SaveAssets();

    //    EditorUtility.FocusProjectWindow();

    //    Selection.activeObject = asset;
    //}

    [MenuItem("Assets/Create/Item")]
    public static void CreateItem()
    {
        Item asset = ScriptableObject.CreateInstance<Item>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Consumable")]
    public static void CreateConsumable()
    {
        Consumable asset = ScriptableObject.CreateInstance<Consumable>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Weapon")]
    public static void CreateWeapon()
    {
        Weapon asset = ScriptableObject.CreateInstance<Weapon>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Misc")]
    public static void CreateMisc()
    {
        Misc asset = ScriptableObject.CreateInstance<Misc>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Armor")]
    public static void CreateArmor()
    {
        Armor asset = ScriptableObject.CreateInstance<Armor>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableItems/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}