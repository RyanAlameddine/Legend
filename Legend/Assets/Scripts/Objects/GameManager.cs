using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEditor;

public delegate void EventDelegate();

public class EventReference
{
    public Type type;
    public EventDelegate func;
    public string eventName;
    public MethodInfo info;
    public object Class;
    public EventDelegate Delegate;

    public EventReference(string eventName, MethodInfo info, Type type)
    {
        this.type = type;
        this.eventName = eventName;
        //this.func = (EventDelegate)Delegate.CreateDelegate(typeof(EventDelegate), info);
        this.info = info;
    }
}


public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Instance {
        get {

            return instance;
        }
    }

    public User user;

    //public string scene;

    //public List<ImageReference> images = new List<ImageReference>();

    public Dictionary<string, Item> itemReferences = new Dictionary<string, Item>();

    public List<EventReference> references = new List<EventReference>();

    Level level;

    public Level CurrentLevel
    {
        get { return level; }
        set { level = value; }
    }


    public void AddClass(object obj)
    {
        Type t = obj.GetType();
        foreach(EventReference reference in references)
        {
            if(reference.type == t)
            {
                reference.Class = obj;
                reference.Delegate = (EventDelegate)Delegate.CreateDelegate(typeof(EventDelegate), obj, reference.info, true);
            }
        }
        //references.Add(new EventReference(ex.eventName, info, ));
    }

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
        Assembly assem = Assembly.GetExecutingAssembly();
        foreach (Type t in assem.GetTypes())
        {
            var s = t.GetMethods();
            foreach (MethodInfo info in s)
            {
                //object ret = info.Invoke(null, new object[0]);
                foreach (object a in info.GetCustomAttributes(true))
                {
                    EventAttribute ex = a as EventAttribute;
                    if (ex != null)
                    {
                        references.Add(new EventReference(ex.eventName, info, t));
                    }
                }
            }
        }

        string[] ScriptableIDs = AssetDatabase.FindAssets("", new string[] { "Assets/ScriptableItems" } );
        //var ScriptableObjects = AssetDatabase.LoadAllAssetsAtPath("Assets/ScriptableItems");
        foreach(string str in ScriptableIDs)
        {
            Item objectItem = AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(str));
            itemReferences.Add(objectItem.name, objectItem);
        }
        DontDestroyOnLoad(gameObject);
        Cursor.visible = true;
        Debug.Log("Data Path: " + Application.persistentDataPath);
    }

    public void runEvent(string name)
    {
        foreach(EventReference r in references)
        {
            if(r.eventName == name)
            {

                //r.func(r.Class);
                r.Delegate();
                //r.info.Invoke(r.Class, new object[0]);
            }
        }
    }
}
