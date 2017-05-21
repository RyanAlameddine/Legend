using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour
{

    public bool view = false;
    public Vector2 endposition = new Vector2(10, 270);
    Vector2 targetposition;
    public Vector2 startingposition = new Vector2(10, 360);
    Vector2 position;

    Vector2 velocity = new Vector2(0, 0);

    float mass = 100f; //mass
    float k = 1f; //spring constant
    float c = 8f; //dampening coefficient

    [SerializeField]
    string eventAction;

    public void Start()
    {
        GameManager.Instance.AddClass(this);
        position = startingposition;
    }

    public void Update()
    {
        //               restoring force                   dampening force
        Vector2 force = k * (targetposition - position) - c * velocity;

        // acceleration changes velocity
        velocity += force / mass;
        // velocity changes position
        position += velocity;

        if (view)
        {
            targetposition = endposition;
            k = 1f;
            c = 8f;
        }
        else
        {
            targetposition = startingposition;
            k = 0.4f;
            c = 2f;
        }
        transform.localPosition = position;
    }

    [Event("closetooltip")]
    public void TrueView(string parameter)
    {
        if (eventAction == parameter)
        {
            view = false;
        }
    }

    [Event("showtooltip")]
    public void FalseView(string parameter)
    {
        if (eventAction == parameter)
        {
            view = true;
        }
    }
}
