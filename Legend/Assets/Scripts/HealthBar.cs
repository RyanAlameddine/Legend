using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    RectTransform rect;
	void Start () {
        rect = (RectTransform)transform;
	}
	
	void Update () {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GameManager.Instance.user.health * 46);
	}
}
