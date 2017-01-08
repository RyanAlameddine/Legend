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
    int facing;

	void Start () {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartMaxSpeed = MaxSpeed;
        facing = 0;
        //0-up
        //1-down
        //2-left
        //3-right
	}
	
	void FixedUpdate () {
        direction = Vector2.zero;

        if (StaminaController.dead)
        {
            MaxSpeed = StartMaxSpeed - 10;
            StaminaController.Stamina += .003f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            MaxSpeed = StartMaxSpeed + 20;
            StaminaController.Stamina -= .005f;
        }
        else
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
            facing = 3;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            if(!setDirection)
            {
                animator.SetTrigger("Left");
            }
            setDirection = true;
            facing = 2;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            if (!setDirection)
            {
                animator.SetTrigger("Up");
            }
            if (!setDirection)
                facing = 0;
            setDirection = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            if (!setDirection)
            {
                animator.SetTrigger("Down");
            }
            if (!setDirection)
                facing = 1;
            setDirection = true;
        }


        if(direction == Vector2.zero)
        {
            currentSpeed = 0;
            if (StaminaController.dead)
            {
                StaminaController.Stamina += .002f;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StaminaController.Stamina += .01f;
                }
                else
                {
                    StaminaController.Stamina += .002f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(facing == 0)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("W");
            }
            else if (facing == 1)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("S");
            }
            else if (facing == 2)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("A");
            }
            else if (facing == 3)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("D");
            }
        }

        animator.SetFloat("Speed", currentSpeed/10);
        rigidbody2d.AddForce(direction * currentSpeed);
    }
}
