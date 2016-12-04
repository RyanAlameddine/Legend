using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour {
    Animator animator;
    Rigidbody2D rigidbody2d;
    [Range(1, 100)]
    public float MaxSpeed = 1;
    float StartMaxSpeed;
    private float currentSpeed = 0;
    Vector2 direction;

	void Start () {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartMaxSpeed = MaxSpeed;
	}
	
	void FixedUpdate () {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MaxSpeed = StartMaxSpeed + 20;
            StaminaController.Stamina -= .005f;
        }else
        {
            MaxSpeed = StartMaxSpeed;
            StaminaController.Stamina += .003f;
        }

        if (currentSpeed < MaxSpeed)
        {
            currentSpeed++;
        }
        else
        {
            currentSpeed = MaxSpeed;
        }
        bool setDirection = false;

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            if (!setDirection)
            {
                animator.SetTrigger("Right");
            }
            setDirection = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            if(!setDirection)
            {
                animator.SetTrigger("Left");
            }
            setDirection = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            if (!setDirection)
            {
                animator.SetTrigger("Up");
            }
            setDirection = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            if (!setDirection)
            {
                animator.SetTrigger("Down");
            }
            setDirection = true;
        }


        if(direction == Vector2.zero)
        {
            currentSpeed = 0;
        }

        animator.SetFloat("Speed", currentSpeed/10);
        rigidbody2d.AddForce(direction * currentSpeed);
    }
}
