using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class AutoTypeUI : MonoBehaviour {
    Text text;
    string endText;
    [SerializeField]
    float seconds;
    float elapsedSeconds;
    int index = 0;
    [HideInInspector]
    public bool pause = true;
    bool tipExists;
    ToolTip tip;

	void Start () {
        text = GetComponent<Text>();
        endText = text.text;
        text.text = "";
        tip = transform.parent.GetComponent<ToolTip>();
        tipExists = tip;
	}
	
	void Update () {
        if (tipExists)
        {
            pause = !tip.view;
        }
        if (!pause) { 
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds > seconds)
            {
                elapsedSeconds = 0;
                text.text = text.text + endText[index];
                index++;
            }
            if (index >= endText.Length) Destroy(this);
        }
	}
}
