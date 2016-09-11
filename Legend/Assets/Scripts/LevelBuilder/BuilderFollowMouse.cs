using UnityEngine;

public class BuilderFollowMouse : MonoBehaviour
{
    public Vector2 GridSize = Vector2.one;

    public Vector3 Offset;

    Collider2D[] colliders;

    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Vector3.forward * Camera.main.transform.position.z);
        //position = new Vector3(Mathf.Round(position.x * GridSize.x) / GridSize.x, Mathf.Round(position.y * GridSize.y) / GridSize.y, position.z);
        transform.position = position + Offset;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
