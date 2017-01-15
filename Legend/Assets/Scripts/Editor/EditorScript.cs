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
            EditorSceneManager.OpenScene("Assets/Scenes/Home.unity");
            EditorApplication.isPlaying = true;
        }
        else
        {
            EditorApplication.isPlaying = false;
        }
    }
}
