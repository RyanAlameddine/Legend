using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
    public Vector2 GridSize = Vector2.one;

    public Vector3 Offset;

    void Update () {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.forward * Camera.main.transform.position.z);
        position = new Vector3(Mathf.RoundToInt(position.x * GridSize.x) / GridSize.x, Mathf.RoundToInt(position.y * GridSize.y) / GridSize.y, position.z);
        transform.position = position + Offset;
	}
}
