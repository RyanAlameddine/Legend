using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{

    public bool portalEnabled = false;
    List<Collider2D> collisions = new List<Collider2D>();
    List<Vector2> playerToPortalCenter = new List<Vector2>();
    List<float> spinRadius = new List<float>();
    List<float> angle = new List<float>();

    public void Start()
    {
        GameManager.Instance.AddClass(this);
    }

    [Event("ShowPortal")]
    public void ShowPortal()
    {
        portalEnabled = true;
    }

    void Update()
    {
        if (portalEnabled == true)
        {
            if (transform.localScale.x < 1 || transform.localScale.y < 1)
            {
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1, 0.05f), Mathf.Lerp(transform.localScale.y, 1, 0.05f), 1);
            }

            for(int i = 0; i < collisions.Count - 1; i++)
            {
                angle[i] += .1f;

                Transform cTrans = collisions[i].gameObject.transform;

                cTrans.localPosition = new Vector2(transform.localPosition.x + spinRadius[i] * (float)Mathf.Cos(angle[i]), transform.localPosition.y + spinRadius[i] * (float)Mathf.Sin(angle[i]) - .2f);

                if (spinRadius[i] > .1f)
                {
                    cTrans.localScale -= new Vector3(0.002f, 0.002f, 0);
                    spinRadius[i] = spinRadius[i] - .005f;
                }
                else
                {
                    //TODO CHANGE LEVEL STUFF
                    if (cTrans.tag == "Player")
                    {
                        cTrans.GetComponent<SpriteRenderer>().enabled = false;
                        portalEnabled = false;
                        StartCoroutine(ChangingScene());
                    }else
                    {
                        Destroy(cTrans);
                    }
                }
            }

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalEnabled)
        {
            if (!collisions.Contains(collision))
            {
                Transform player = collision.gameObject.transform;
                Animator anim = player.GetComponent<Animator>();
                if (anim)
                {
                    anim.enabled = false;
                }

                collisions.Add(collision);
                playerToPortalCenter.Add(new Vector2(player.localPosition.x - transform.localPosition.x, player.localPosition.y - transform.localPosition.y));
                spinRadius.Add(new Vector2(player.localPosition.x - transform.localPosition.x, player.localPosition.y - transform.localPosition.y).magnitude);
                angle.Add(Mathf.Rad2Deg * (float)Mathf.Atan2(player.localPosition.y - transform.localPosition.y, player.localPosition.x - transform.localPosition.x));
            }
        }
    }

    IEnumerator ChangingScene()
    {
        Camera c = Camera.main;
        yield return new WaitForSeconds(2);
        while (transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(.001f, .001f, 0);
            yield return new WaitForEndOfFrame();
        }
        while(c.transform.localPosition.z < 10)
        {
            c.transform.localPosition += new Vector3(0, 0, .001f);
        }
    }
}
