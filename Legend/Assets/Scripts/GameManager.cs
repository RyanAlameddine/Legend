using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public static User user;

    public static List<ImageReference> imageReferences = new List<ImageReference>();

    public List<ImageReference> images = new List<ImageReference>();

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
        imageReferences = images;
        Debug.Log("Data Path: " + Application.persistentDataPath);
    }
}
