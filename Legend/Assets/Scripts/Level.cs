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
    [HideInInspector]
    public int[,] levelTiles;
    public bool connectedToImage = false;

    void Start()
    {
        GameManager.Instance.CurrentLevel = this;
        if (!connectedToImage)
        {
            initializeLevelTiles();
            loadLevel();
        }
    }

    public void loadLevel()
    {
        foreach (CoordinateFloat CFloat in coordinateFloats)
        {
            levelTiles[(int)CFloat.Coordinate.x, (int)CFloat.Coordinate.y] = (int)CFloat.Float;
        }
        #region Border
        if (border != null)
        {
            Vector2 objectSize = border.GetComponent<BoxCollider2D>().size;
            for (int row = 0; row < Height / objectSize.y; row++)
            {
                for (int col = 0; col < Width / objectSize.x; col++)
                {
                    if (col == 0 || row == 0 ||
                        col == (int)(Width / objectSize.x) - 1 || row == (int)(Height / objectSize.y) - 1)
                    {
                        ((GameObject)Instantiate(border, new Vector3(col * objectSize.x + Center.x,
                                                                        row * objectSize.y + Center.y, 0), Quaternion.identity)).transform.SetParent(levelparent);
                    }
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
                i = levelTiles[col, row];
                //Vector2 coord = new Vector2(col, row);
                //foreach(CoordinateFloat CFloat in coordinateFloats)
                //{
                //    if(CFloat.Coordinate == coord)
                //    {
                //        i = (int) CFloat.Float;
                //    }
                //}
                ((GameObject)Instantiate(objects[i], new Vector3((col) + Center.x, row + Center.y, 0), Quaternion.identity)).transform.SetParent(levelparent);
            }
        }
        #endregion
    }

    public int GetTile(Vector2 position)
    {
        return levelTiles[(int)(position.x + 0.5f - Center.x), (int)(position.y - Center.y)];
    }

    public void initializeLevelTiles()
    {
        levelTiles = new int[Width, Height];
    }
}
