using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CycleImage : MonoBehaviour {
    public List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer spriteRenderer;
    Image image;
    int j = 0;
    float time;
    public float timebetween;
    public RendererType type;

	// Use this for initialization
	void Start () {
        if (type == RendererType.UI)
        {
            image = GetComponent<Image>();
            image.sprite = sprites[0];
        }else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= timebetween)
        {
            time -= timebetween;
            j++;
            if (j >= sprites.Count)
            {
                j = 0;
            }
            if (type == RendererType.UI) {
                image.sprite = sprites[j];
            }else
            {
                spriteRenderer.sprite = sprites[j];
            }
        }
	}
}
