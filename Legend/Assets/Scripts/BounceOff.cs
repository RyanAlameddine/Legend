using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class BounceOff : MonoBehaviour
{
    public float Force;
    [SerializeField]
    LayerMask layerMask;
    Rigidbody2D body2D;

    public void Reset()
    {
        layerMask = LayerMask.NameToLayer("Everything");
    }

    public void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((layerMask.value & ( 1 << collision.gameObject.layer)) != 0 || layerMask.value == -1)
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (body2D)
                {
                    body2D.AddForceAtPosition((body2D.position - collision.contacts[i].point).normalized * Force / collision.contacts.Length, collision.contacts[i].point, ForceMode2D.Impulse);
                }
                if (collision.rigidbody)
                {
                    collision.rigidbody.AddForceAtPosition((collision.rigidbody.position - collision.contacts[i].point).normalized * Force / collision.contacts.Length, collision.contacts[i].point, ForceMode2D.Impulse);
                }
            }
        }
    }

}
