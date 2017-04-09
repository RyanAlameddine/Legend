using UnityEngine;
using System.Collections;

[System.Serializable]
public class CoordinateFloat {
    public Vector2 Coordinate;
    public float Float;

    public CoordinateFloat(Vector2 Coordinate, float Float)
    {
        this.Coordinate = Coordinate;
        this.Float = Float;
    }
}
