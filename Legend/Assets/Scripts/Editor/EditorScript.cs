using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class EditorScript : EditorWindow
{

    [MenuItem("Play/PlayStartScene _%h")]
    public static void RunMainScene()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            AssetDatabase.SaveAssets();
            EditorSceneManager.OpenScene("Assets/Scenes/Home.unity");
            EditorApplication.isPlaying = true;
        }
        else
        {
            EditorApplication.isPlaying = false;
        }
    }

    [MenuItem("SceneSwitch/Home _F1")]
    public static void Home()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Home.unity");
    }

    [MenuItem("SceneSwitch/Level1 _F2")]
    public static void Level1()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
    }

    [MenuItem("SceneSwitch/LoadedScene _F3")]
    public static void LoadedScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/LoadedScene.unity");
    }

    [MenuItem("SceneSwitch/Endless _F4")]
    public static void Endless()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Endless.unity");
    }

    [MenuItem("SceneSwitch/LevelBuilder _F5")]
    public static void LevelBuilder()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/LevelBuilder.unity");
    }
}