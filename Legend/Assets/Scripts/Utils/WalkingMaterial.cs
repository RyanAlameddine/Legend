using UnityEngine;
using System.Collections;

public class WalkingMaterial : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller)
        {
            controller.SetSwimming(false);
        }
    }
}
