using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(ItemStats))]
public class ItemOnFloor : MonoBehaviour
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
                GameManager.Instance.user.items.Add(GetComponent<ItemStats>().item);
                Destroy(transform.gameObject);
                Inventory.Instance.resetInv();                
            }
        }
    }
}
