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
            transform.position = level != null ? new Vector3(Mathf.Clamp(transform.position.x, level.Center.x - level.Width/2 + cam.orthographicSize * cam.aspect + clampoffset.x, level.Center.x + level.Width / 2 - cam.orthographicSize * cam.aspect - clampoffset.x), 
                                            Mathf.Clamp(transform.position.y, level.Center.y - level.Height / 2 + cam.orthographicSize + clampoffset.y, level.Center.y + level.Height / 2 - cam.orthographicSize + clampoffset.y),
                                            transform.position.z) : transform.position;
        }
        if (TrackRotation)
        {
            transform.rotation = target.rotation;
        }
	}
}
