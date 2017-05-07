using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraTrack : MonoBehaviour {
    [SerializeField]
    Transform target;

    [SerializeField]
    Level level;

    public bool TrackRotation;
    public bool TrackPosition;

    [SerializeField]
    Vector3 offset;
    [SerializeField]
    Vector2 clampoffset;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
	
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
