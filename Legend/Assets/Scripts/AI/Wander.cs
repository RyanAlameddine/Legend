using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BehaviorBase {
    System.Random rand = new System.Random();
    [SerializeField]
    Vector2 waitTimeRange;
    [SerializeField]
    Vector2 walkTimeRange;
    bool toRunWander = false;
    Vector2 direction;
    Rigidbody2D rb;
    bool moving = false;
    [SerializeField]
    float speedMultiplier = 20;
    bool running = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override float run()
    {
        toRunWander = true;
        if (moving)
        {
            return 1;
        }
        else
        {
            return 0.1f;
        }
    }

    public void Update()
    {
        if (toRunWander)
        {
            if(!running)
                StartCoroutine(wander());
            running = true;
        }else
        {
            StopCoroutine(wander());
            running = false;
            moving = false;
        }
        toRunWander = false;

        if (moving)
        {
            rb.AddForce(direction * speedMultiplier);
        }
    }

    public override bool check()
    {
        return true;
    }

    IEnumerator<YieldInstruction> wander()
    {
        while (true)
        {
            if(rand.Next(0, 4) != 0)
                yield return new WaitForSeconds(rand.Next((int)waitTimeRange.x*10, (int)waitTimeRange.y*10)/10);
            calculateDirection();
            moving = true;
            yield return new WaitForSeconds(rand.Next((int)walkTimeRange.x * 10, (int)walkTimeRange.y * 10) / 10);
            moving = false;
        }
    }

    void calculateDirection()
    {
        int dir = rand.Next(1, 5);
        if (dir == 1)
        {
            direction = Vector2.up;
        } else if (dir == 2)
        {
            direction = Vector2.down;
        } else if (dir == 3)
        {
            direction = Vector2.left;
        } else if (dir == 4)
        {
            direction = Vector2.right;
        }
    }

    public override Vector2 getDirection()
    {
        return direction;
    }
}
