using UnityEngine;
using System.Collections;

public class DisableScript : MonoBehaviour
{
    public MonoBehaviour disable;
    void Start()
    {
        disable.enabled = false;
    }

    public void OnDestroy()
    {
        disable.enabled = true;
    }
}
