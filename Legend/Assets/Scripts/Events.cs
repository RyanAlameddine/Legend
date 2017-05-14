using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEditor;

public delegate void EventDelegate(String str);

public class EventReference
{
    public Type type;
    //public EventDelegate func;
    public string eventName;
    public MethodInfo info;
    public List<object> Classes;
    public List<EventDelegate> Delegates;

    public EventReference(string eventName, MethodInfo info, Type type)
    {
        Delegates = new List<EventDelegate>();
        Classes = new List<object>();
        this.type = type;
        this.eventName = eventName;
        //this.func = (EventDelegate)Delegate.CreateDelegate(typeof(EventDelegate), info);
        this.info = info;
    }

    public void AddClass(object obj)
    {
        Classes.Add(obj);

        Delegates.Add((EventDelegate)Delegate.CreateDelegate(typeof(EventDelegate), obj, info, true));
    }

    public void Invoke(String param)
    {
        foreach(EventDelegate del in Delegates)
        {
            del(param);
        }
    }
}
