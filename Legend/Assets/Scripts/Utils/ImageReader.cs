using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageReader : MonoBehaviour {
    [SerializeField]
    Texture2D loadingImage;
    [SerializeField]
    Level level;
    [SerializeField]
    List<ColorReference> colorReferences;
    Dictionary<int, int> a = new Dictionary<int, int>(); //Tile
    Dictionary<int, int> r = new Dictionary<int, int>(); //Obstacle
    Dictionary<int, int> g = new Dictionary<int, int>(); //Object
    Dictionary<int, int> b = new Dictionary<int, int>();
    [SerializeField]
    GameObject[] Tiles;
    [SerializeField]
    GameObject[] Obstacles;
    [SerializeField]
    GameObject[] Objects;

    CameraTrack track;
    [SerializeField]
    Camera Camera;
    System.Random random = new System.Random();

    [SerializeField]
    GameObject ExitPortal;

    GameObject UI;
    [SerializeField]
    GameObject stamina;
    [SerializeField]
    GameObject shade;
    [SerializeField]
    GameObject health;
    [SerializeField]
    List<GameObject> tooltip;

    void Start () {
        track = Instantiate(Camera).GetComponent<CameraTrack>();
        UI = new GameObject();
        UI.AddComponent<RectTransform>();
        UI.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        UI.AddComponent<CanvasScaler>();
        UI.AddComponent<GraphicRaycaster>();

        shade = Instantiate(shade);
        shade.transform.SetParent(UI.transform);
        RectTransform rectTransform = shade.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);

        stamina = Instantiate(stamina);
        stamina.transform.SetParent(UI.transform);
        stamina.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -40);

        health = Instantiate(health);
        health.transform.SetParent(UI.transform);
        health.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        foreach (GameObject o in tooltip)
        {
            if (o)
            {
                Instantiate(o).transform.SetParent(UI.transform);
            }
        }

        shade.GetComponent<Image>().color = Color.black;
        foreach(ColorReference cr in colorReferences)
        {
            int cF = (int) (cr.colorFloat * 255);
            int oI = (int) cr.objectIndex;
            switch (cr.colorType)
            {
                case ColorReference.ColorType.a:
                    a.Add(cF, oI);
                    break;
                case ColorReference.ColorType.r:
                    r.Add(cF, oI);
                    break;
                case ColorReference.ColorType.g:
                    g.Add(cF, oI);
                    break;
                case ColorReference.ColorType.b:
                    b.Add(cF, oI);
                    break;
            }
        }
        level.Width = loadingImage.width;
        level.Height = loadingImage.height;
        level.initializeLevelTiles();
        for(int x = 0; x < loadingImage.width; x++)
        {
            for (int y = 0; y < loadingImage.height; y++)
            {
                ColorData data = translateColor(loadingImage.GetPixel(x, y));
                level.levelTiles[x, y] = data.Tile;
                if(data.Object != null)
                {
                    GameObject obj = (GameObject) Instantiate(data.Object, new Vector3(x + level.Center.x, y + level.Center.y, 0), Quaternion.identity);
                    if (obj.transform.childCount > 0 && obj.transform.GetChild(0).tag == "Player")
                    {
                        obj.transform.position = Vector3.zero;
                        obj.transform.GetChild(0).transform.localPosition = new Vector3(x + level.Center.x, y + level.Center.y, 0);
                        track.target = obj.transform.GetChild(0);
                        Instantiate(ExitPortal, new Vector3(x + level.Center.x, y + level.Center.y, 0), Quaternion.identity);
                    }
                    else
                    {
                        obj.transform.SetParent(level.levelparent);
                    }
                }
                else if (data.Obstacle != null)
                {
                    GameObject obj = (GameObject)Instantiate(data.Obstacle, new Vector3(x + level.Center.x + random.Next(-2, 2)/100f, y + level.Center.y + random.Next(-2, 2) / 100f, 0), Quaternion.identity);
                    obj.transform.SetParent(level.levelparent);
                }
            }
        }
        level.Tiles = Tiles;
        level.loadLevel();
	}

    ColorData translateColor(Color c)
    {
        ColorData data = new ColorData();
        int aVal = (int) (c.a * 255);
        int rVal = (int) (c.r * 255);
        int gVal = (int) (c.g * 255);
        if (a.ContainsKey(aVal))
            data.Tile = a[aVal];                        //A
        if (r.ContainsKey(rVal))
            data.Obstacle = Obstacles[r[rVal]];         //R
        if (g.ContainsKey(gVal))
            data.Object = Objects[g[gVal]];             //G
        return data;
    }
}

struct ColorData
{
    public int Tile;    //A
    public GameObject Obstacle;//R
    public GameObject Object;  //G
}
