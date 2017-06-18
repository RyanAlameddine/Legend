using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBase : MonoBehaviour{

    public virtual float run()
    {
        return 0f;
    }

    public virtual bool check()
    {
        return false;
    }
    
    public virtual Vector2 getDirection()
    {
        return Vector2.up;
    }
}
