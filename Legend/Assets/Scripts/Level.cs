using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Vector2 Center;
    [Range(5, 100)]
    public int Width = 5;
    [Range(5, 100)]
    public int Height = 5;
    public GameObject border;
    [SerializeField]
    Transform levelparent;
    [SerializeField]
    GameObject[] objects;
    [SerializeField]
    List<CoordinateFloat> coordinateFloats;

    void Start()
    {
        #region Border
        Vector2 objectSize = border.GetComponent<BoxCollider2D>().size;
        for (int row = 0; row < Height / objectSize.y; row++)
        {
            for (int col = 0; col < Width / objectSize.x; col++)
            {
                if (col == 0 || row == 0 ||
                    col == (int)(Width / objectSize.x) - 1 || row == (int)(Height / objectSize.y) - 1)
                {
                    ((GameObject)Instantiate(border, new Vector3((col - Width / objectSize.x / 2 + .5f) * objectSize.x + Center.x,
                                                                    (row - Height / objectSize.y / 2 + .5f) * objectSize.y + Center.y, 0), Quaternion.identity)).transform.SetParent(levelparent);
                }
            }
        }
        #endregion
        #region BackGround
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width - 1; col++)
            {
                int i = 0;
                Vector2 coord = new Vector2(col, row);
                foreach(CoordinateFloat CFloat in coordinateFloats)
                {
                    if(CFloat.Coordinate == coord)
                    {
                        i = (int) CFloat.Float;
                    }
                }
                ((GameObject)Instantiate(objects[i], new Vector3((col - Width / 2 + .5f) + Center.x, (row - Height / 2 + .5f) + Center.y, 0), Quaternion.identity)).transform.SetParent(levelparent);
            }
        }
        #endregion
    }
}
