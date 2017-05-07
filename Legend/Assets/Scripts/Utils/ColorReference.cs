using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorReference {
    public float objectIndex;
    public enum ColorType
    {
        a,
        r,
        g,
        b
    }
    public ColorType colorType;
    public float colorFloat;

    public ColorReference(float objectIndex, ColorType colorType, float colorFloat)
    {
        this.objectIndex = objectIndex;
        this.colorFloat = colorFloat;
        this.colorType = colorType;
    }
}
