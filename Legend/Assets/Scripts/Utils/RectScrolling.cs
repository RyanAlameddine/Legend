using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RectScrolling : MonoBehaviour {
    RectTransform rect;
    Text text;
    [SerializeField]
    Vector2 PerSecond;
    [SerializeField]
    float APerSecond;
	void Start () {
        rect = (RectTransform)transform;
        text = GetComponent<Text>();
	}
	
	void Update () {
        rect.anchoredPosition += PerSecond * Time.deltaTime;
        if (text)
        {
            text.color += new Color(0, 0, 0, APerSecond * Time.deltaTime);
        }
	}
}
