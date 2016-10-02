using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public static User user;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("Data Path: " + Application.persistentDataPath);
    }

}
