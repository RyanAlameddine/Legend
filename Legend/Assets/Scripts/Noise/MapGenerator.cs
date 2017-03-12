using UnityEngine;
using System.Collections;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    public int seed;

    public Transform playerTransform;

    public Renderer textureRenderer;

    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode drawMode;

    public const int mapChunkSize = 50;
    public int noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    public bool autoUpdate;

    [SerializeField]
    CombineMap map;

    //public TerrainType[] regions;

    public MapData mapData;

    public enum Mode { Heat, Moisture};
    public Mode mode;

    void Start()
    {
        //seed = Random.Range(0, int.MaxValue);
        DrawMap();
    }

    public void DrawMap()
    {
        mapData = GenerateMapData();

        if (drawMode == DrawMode.NoiseMap)
        {
            Texture2D texture = TextureGenerator.TextureFromHeightMap(mapData.heightMap);
            SetTexture(texture);
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            Texture2D texture = TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize);
            SetTexture(texture);
        }
    }

    void SetTexture(Texture2D texture)
    {
        Material tempMaterial = null;
        if (textureRenderer.sharedMaterial == null)
        {
            tempMaterial = new Material(Shader.Find("Unlit/Texture"));
        }
        else
        {
            tempMaterial = new Material(textureRenderer.sharedMaterial);
        }
        tempMaterial.mainTexture = texture;
        textureRenderer.sharedMaterial = tempMaterial;
        textureRenderer.transform.localScale = new Vector3(5, 1, 5);
    }

    MapData GenerateMapData()
    {
        Tile[,] noiseMap = Noise.GeterateNoiseMap(mapChunkSize, mapChunkSize, noiseScale, seed, octaves, persistance, lacunarity, offset, mode, false);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                //float currentHeight = noiseMap[x, y].value;
                if (mode == Mode.Heat)
                {
                    switch (noiseMap[x, y].HeatType)
                    {
                        case HeatType.Coldest:
                            colorMap[y * mapChunkSize + x] = Region.Coldest;
                            break;
                        case HeatType.Colder:
                            colorMap[y * mapChunkSize + x] = Region.Colder;
                            break;
                        case HeatType.Cold:
                            colorMap[y * mapChunkSize + x] = Region.Cold;
                            break;
                        case HeatType.Warm:
                            colorMap[y * mapChunkSize + x] = Region.Warm;
                            break;
                        case HeatType.Warmer:
                            colorMap[y * mapChunkSize + x] = Region.Warmer;
                            break;
                        case HeatType.Warmest:
                            colorMap[y * mapChunkSize + x] = Region.Hot;
                            break;
                    }
                }else
                {
                    switch (noiseMap[x, y].MoistureType)
                    {
                        case MoistureType.Dryest:
                            colorMap[y * mapChunkSize + x] = Region.Dryest;
                            break;
                        case MoistureType.Dryer:
                            colorMap[y * mapChunkSize + x] = Region.Dryer;
                            break;
                        case MoistureType.Dry:
                            colorMap[y * mapChunkSize + x] = Region.Dry;
                            break;
                        case MoistureType.Wet:
                            colorMap[y * mapChunkSize + x] = Region.Wet;
                            break;
                        case MoistureType.Wetter:
                            colorMap[y * mapChunkSize + x] = Region.Wetter;
                            break;
                        case MoistureType.Wettest:
                            colorMap[y * mapChunkSize + x] = Region.Wettest;
                            break;
                    }
                }
            }
        }

        return new MapData(noiseMap, colorMap);
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            lacunarity = 0;
        }
        map.Start();
        map.DrawMap();
    }

    public void DrawTexture(Texture2D texture)
    {

        textureRenderer.material.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(5, 1, 5);
    }


}


public struct MapData
{
    public readonly Tile[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(Tile[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}

//[System.Serializable]
//public struct TerrainType
//{
//    public float height;
//    public Color color;
//}

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.DrawMap();
            }
        }
        if (GUILayout.Button("Generate"))
        {
            mapGen.DrawMap();
        }
    }
}
