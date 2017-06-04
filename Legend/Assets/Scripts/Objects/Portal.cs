using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public bool portalEnabled = false;
    List<Collider2D> collisions = new List<Collider2D>();
    List<Vector2> playerToPortalCenter = new List<Vector2>();
    List<float> spinRadius = new List<float>();
    List<float> angle = new List<float>();
    SpriteRenderer sr;
    Image screenTint;
    [SerializeField]
    int scene;
    [SerializeField]
    int eventCalls = 1;
    int callsCount = 0;

    public void Start()
    {
        GameManager.Instance.AddClass(this);
        sr = GetComponent<SpriteRenderer>();
        screenTint = GameObject.FindGameObjectWithTag("Shade").GetComponent<Image>();
    }

    [Event("ShowPortal")]
    public void ShowPortal(string parameter)
    {
        callsCount++;
        if(callsCount >= eventCalls)
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
                    cTrans.localScale -= new Vector3(0.003f, 0.003f, 0);
                    spinRadius[i] = spinRadius[i] - .005f;
                }
                else
                {
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
                angle.Add(Mathf.Atan2(player.localPosition.y - transform.localPosition.y, player.localPosition.x - transform.localPosition.x));
            }
        }
    }

    IEnumerator ChangingScene()
    {
        Camera c = Camera.main;
        yield return new WaitForSeconds(2);
        GameManager.Instance.runEvent("TransitionToLevel");
        while (transform.localScale.x > 0.03)
        {
            transform.localScale -= new Vector3(.01f, .01f, 0);
            yield return new WaitForEndOfFrame();
        }
        c.transform.position = transform.position;
        sr.enabled = false;
        screenTint.color = Color.black;
        screenTint.color -= new Color(0, 0, 0, 1);
        while (c.orthographicSize > 0)
        {
            c.orthographicSize -= .02f;
            c.transform.Rotate(new Vector3(0, 0, 3f * (1-(c.orthographicSize/3))));
            screenTint.color += new Color(0, 0, 0, .005f);
            yield return new WaitForEndOfFrame();
        }
        while(screenTint.color.a < 1)
        {
            screenTint.color += new Color(0, 0, 0, .005f);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(scene);
    }
}
