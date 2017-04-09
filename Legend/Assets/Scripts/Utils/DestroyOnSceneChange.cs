using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DestroyOnSceneChange : MonoBehaviour
{
    public List<MonoBehaviour> behaviors;
    string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().path;
    }

    void Update()
    {
        if(scene != SceneManager.GetActiveScene().path)
        {
            foreach(MonoBehaviour behavior in behaviors)
            {
                Destroy(behavior);
            }
        }
    }
}
