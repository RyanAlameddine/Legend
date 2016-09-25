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

    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }
	
	void Update () {
        if (TrackPosition)
        {
            transform.position = target.position + offset;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, level.Center.x - level.Width/2 + camera.orthographicSize * camera.aspect + clampoffset.x, level.Center.x + level.Width / 2 - camera.orthographicSize * camera.aspect - clampoffset.x), 
                                            Mathf.Clamp(transform.position.y, level.Center.y - level.Height / 2 + camera.orthographicSize + clampoffset.y, level.Center.y + level.Height / 2 - camera.orthographicSize + clampoffset.y),
                                            transform.position.z);
        }
        if (TrackRotation)
        {
            transform.rotation = target.rotation;
        }
	}
}
