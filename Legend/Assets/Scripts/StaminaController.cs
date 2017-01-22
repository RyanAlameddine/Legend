using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour {
    public static float Stamina = 1;
    public static bool dead;
    public static bool lockcolor;
    Image image;
    public float startHue;
    public float endHue;
    float hue = 1;
    float saturation = 1;
    float value = 1;
    public bool colorChange = true;

    void Start () {
        hue = startHue;
        image = GetComponent<Image>();
        if(colorChange)
        image.color = Color.HSVToRGB(hue, saturation, value);
    }
	
	void Update () {
        Stamina = Mathf.Clamp(Stamina, 0, 1);
        image.fillAmount = Stamina;
        if (colorChange)
        {
            if (!lockcolor)
            {
                image.color = Color.HSVToRGB(Mathf.Lerp(endHue, startHue, Stamina), saturation, value);
            }
            if (Stamina <= 0)
            {
                dead = true;
                lockcolor = true;
            }
            if (Stamina >= 1)
            {
                dead = false;
                lockcolor = false;
            }
        }
	}
}
