using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
    [SerializeField]
    Transform target;

    public bool TrackRotation;
    public bool TrackPosition;

    [SerializeField]
    Vector3 offset;
	
	void Update () {
        if (TrackPosition)
        {
            transform.position = target.position + offset;
        }
        if (TrackRotation)
        {
            transform.rotation = target.rotation;
        }
	}
}
