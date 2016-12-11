using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class EventReference
{
    string eventName;
    MethodInfo info;
    public EventReference(string eventName, MethodInfo info)
    {
        this.eventName = eventName;
        this.info = info;
    }
}

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public User user;

    public List<ImageReference> images = new List<ImageReference>();

    public List<EventReference> references = new List<EventReference>();

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
        Cursor.visible = true;
        Debug.Log("Data Path: " + Application.persistentDataPath);

        Assembly assem = Assembly.GetExecutingAssembly();
        foreach(Type t in assem.GetTypes())
        {
            foreach(MethodInfo info in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                //object ret = info.Invoke(null, new object[0]);
                foreach(object a in info.GetCustomAttributes(true)){
                    EventAttribute ex = a as EventAttribute;
                    if (ex != null)
                    {
                        references.Add(new EventReference(ex.eventName, info));
                    }
                }
            }
        }
    }


}
