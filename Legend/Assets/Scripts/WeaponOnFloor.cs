using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponOnFloor : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField]
    Collider2D PlayerCollider;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (box.IsTouching(PlayerCollider))
            {
                
            }
        }
    }
}
