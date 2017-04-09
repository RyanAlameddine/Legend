using UnityEngine;
using System.Collections;

public class UIFloat : MonoBehaviour {
    RectTransform rect;
    Vector2 startposition;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float scale = 1;
    float x = 0;
	void Start () {
        rect = (RectTransform)transform;
        startposition = rect.anchoredPosition;
	}
	
	void Update () {
        x += speed * Time.deltaTime;
        rect.anchoredPosition = new Vector2(startposition.x, startposition.y + Mathf.Cos(x) * scale);
	}
}
