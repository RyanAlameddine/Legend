using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CycleImageUI : MonoBehaviour {
    public List<Sprite> sprites = new List<Sprite>();
    Image image;
    int j = 0;
    float time;
    public float timebetween;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
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
            image.sprite = sprites[j];
        }
	}
}
