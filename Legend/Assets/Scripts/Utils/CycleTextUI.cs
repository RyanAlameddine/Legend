using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CycleTextUI : MonoBehaviour {
    public List<string> strings = new List<string>();
    Text text;
    int j = 0;
    float time;
    public float timebetween;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = strings[0];
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= timebetween)
        {
            time -= timebetween;
            j++;
            if (j >= strings.Count)
            {
                j = 0;
            }
            text.text = strings[j];
        }
	}
}
