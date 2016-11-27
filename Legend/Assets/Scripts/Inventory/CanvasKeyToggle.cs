using UnityEngine;
using System.Collections;

public class CanvasKeyToggle : MonoBehaviour {
    public KeyCode key;

	void Update () {
        if (Input.GetKeyDown(key))
        {
            Canvas disable = GetComponent<Canvas>();
            disable.enabled = !disable.enabled;
        }
	}
}
