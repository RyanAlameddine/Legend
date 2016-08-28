using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Layering : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update () {
        spriteRenderer.sortingOrder = (int)(-transform.position.y * 10);
	}
}
