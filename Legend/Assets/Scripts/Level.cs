using UnityEngine;
using System.Collections;

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
    GameObject background;
    [SerializeField]
    int bgtilesize;

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
        for (int row = 0; row < Height / bgtilesize; row++)
        {
            for (int col = 0; col < Width / bgtilesize - 1; col++)
            {
                ((GameObject)Instantiate(background, new Vector3((col - Width / bgtilesize / 2 + .5f) * bgtilesize + Center.x, (row - Height / bgtilesize / 2 + .5f) * bgtilesize + Center.y, 0), Quaternion.identity)).transform.SetParent(levelparent);
            }
        }
        #endregion
    }

    void Update()
    {

    }
}
