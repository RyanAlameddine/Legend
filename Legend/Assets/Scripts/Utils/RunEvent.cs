using UnityEngine;
using System.Collections;

public class RunEvent : MonoBehaviour
{
    public KeyCode Key;
    public string EventName;
    public bool disableafter;
    public bool whenDestroyed;
    public bool onTriggerEnter;
    public bool onTriggerLeave;
    public string overrideParameter;

    void Run(string parameter)
    {
        GameManager.Instance.runEvent(EventName, overrideParameter == "" ? parameter : overrideParameter);
        if (disableafter)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Key != KeyCode.None && Input.GetKeyDown(Key))
        {
            Run("Key_Down_" + Key.ToString());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerEnter) Run("Hover");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (onTriggerLeave) Run("Hover");
    }

    public void OnDestroy()
    {
        if (whenDestroyed)
        {
            Run("Destroy");
        }
    }
}
