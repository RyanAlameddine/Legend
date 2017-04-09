using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Layering : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    int start;
    public bool disable;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        start = spriteRenderer.sortingOrder;
    }

    void Update () {
        spriteRenderer.sortingOrder = (int)(-transform.position.y * 10);
        if (disable)
        {
            spriteRenderer.sortingOrder = start;
        }
	}
}
