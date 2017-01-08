using UnityEngine;
using System.Collections;

public class InputRunEvent : MonoBehaviour {
    public KeyCode Key;
    public string EventName;
    public bool disableafter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(Key))
        {
            GameManager.Instance.runEvent(EventName);
            if (disableafter)
            {
                Destroy(this);
            }
        }
	}
}
