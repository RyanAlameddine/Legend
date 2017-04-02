using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageUnTinter : MonoBehaviour {
    [SerializeField]
    Image image;
    Color previousColor;
    [SerializeField]
    Color resetColor;
    [SerializeField]
    float speed = .02f;
    bool changedPreviously = false;

	void Start () {
        image = GetComponent<Image>();
        previousColor = image.color;
	}
	
	void Update () {
        changedPreviously = false;
        if(previousColor == image.color || changedPreviously)
        {
            image.color = Color.Lerp(image.color, resetColor, speed);
            if(Mathf.Abs(image.color.a - resetColor.a) < .05f && Mathf.Abs(image.color.b - resetColor.b) < .05f && Mathf.Abs(image.color.g - resetColor.g) < .05f && Mathf.Abs(image.color.r - resetColor.r) < .05f)
            {
                image.color = resetColor;
                changedPreviously = false;
            }
            changedPreviously = true;
        }
        previousColor = image.color;
	}
}
