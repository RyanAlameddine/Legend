using UnityEngine;
using System.Collections;

public class ExitPortal : MonoBehaviour {

    float angle = 0;
    float spinRadius = 0;
    Transform player;
    bool running = true;
    float x = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.localScale = new Vector3(0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            if (transform.localScale.x < 1.5 || transform.localScale.y < 1.5)
            {
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1, 0.01f), Mathf.Lerp(transform.localScale.y, 1, 0.01f), 1);
            }

            angle += .1f;

            player.localPosition = new Vector2(transform.localPosition.x + spinRadius * (float)Mathf.Cos(angle), transform.localPosition.y + spinRadius * (float)Mathf.Sin(angle) - .2f);

            if (spinRadius < 1.5f)
            {
                spinRadius = spinRadius + .005f;
            }
            else
            {
                running = false;
            }
            if (player.localScale.x < 1 || player.localScale.y < 1)
            {
                player.localScale += new Vector3(0.007f, 0.007f, 0);
            }
            else
            {
                player.localScale = Vector3.one;
            }
        }else
        {
            x += Time.deltaTime;
            if(x >= 1)
            {
                if (transform.localScale.x < .01f) Destroy(gameObject);
                transform.localScale -= new Vector3(0.003f, 0.003f, 0);
            }
        }
    }
}
