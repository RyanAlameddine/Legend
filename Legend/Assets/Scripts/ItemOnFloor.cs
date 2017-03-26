using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class ItemOnFloor : MonoBehaviour
{
    BoxCollider2D box;
    Collider2D PlayerCollider;
    public Item item;
    SpriteRenderer sr;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.sprite;
        PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (box.IsTouching(PlayerCollider))
            {
                GameManager.Instance.user.items.Add(item.name);
                Destroy(transform.gameObject);
                Inventory.Instance.resetInv();                
            }
        }
    }
}
