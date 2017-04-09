using UnityEngine;
using System.Collections;

public class RunEvent : MonoBehaviour
{
    public KeyCode Key;
    public string EventName;
    public bool disableafter;
    public bool whenDestroyed;

    void Run()
    {
        GameManager.Instance.runEvent(EventName);
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
            Run();
        }
    }

    public void OnDestroy()
    {
        if (whenDestroyed)
        {
            Run();
        }
    }
}
