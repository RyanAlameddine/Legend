using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class AI : MonoBehaviour {
    public List<BehaviorBase> behaviors;
    BehaviorBase currentBehavior;
    Animator animator;
    

    public void Start()
    {
        animator = GetComponent<Animator>();
        foreach(BehaviorBase be in behaviors)
        {
            if (be.check()) currentBehavior = be;
        }
    }

    public void Update()
    {
        animator.SetFloat("Speed", currentBehavior.run());
        Vector2 direction = currentBehavior.getDirection();
        animator.SetTrigger(direction == Vector2.up ? "Up" : direction == Vector2.down ? "Down" : direction == Vector2.left ? "Left" : "Right");
    }

    public void FixedUpdate()
    {
        foreach (BehaviorBase be in behaviors)
        {
            if (be.check()) currentBehavior = be;
        }
    }
}
