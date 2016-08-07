using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour {
    Animator animator;
    Rigidbody2D rigidbody2d;
    [SerializeField]
    float Speed = 1;
    Vector2 direction;
    bool moved;
	void Start () {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            moved = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            moved = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            moved = true;
        }
        if (!moved)
        {
            direction += Vector2.zero;
        }
        animator.SetBool("Up", Input.GetKey(KeyCode.W));
        animator.SetBool("Down", Input.GetKey(KeyCode.S));
        animator.SetBool("Left", Input.GetKey(KeyCode.A));
        animator.SetBool("Right", Input.GetKey(KeyCode.D));
        rigidbody2d.AddForce(direction * Speed);
        moved = false;
    }
}
