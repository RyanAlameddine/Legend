using UnityEngine;
using System.Collections;
using System;

[AttributeUsage(AttributeTargets.Method)]
public class EventAttribute : Attribute {
    public string eventName;
    public EventAttribute(String eventName)
    {
        this.eventName = eventName;
    }
}
