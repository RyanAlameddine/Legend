using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller && collision.collider2D.) //CHECK IF FULLY CONTAINS PLAYER COLLIDER
        {
            controller.SetSwimming(true);
        }
    }

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    PlayerController controller = collision.GetComponent<PlayerController>();
    //    if (controller)
    //    {
    //        controller.SetSwimming(false);
    //    }
    //}
}
