using UnityEngine;
using System.Collections;
using UnityEditor;

public class CombineMap : MonoBehaviour
{

    public MapGenerator heatMapGenerator;
    public MapGenerator moistureMapGenerator;

    MapData heatMap;
    MapData moistureMap;

    public MapData mapData;

    public Renderer textureRenderer;

    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode drawMode;

    public const int mapChunkSize = 250;

    public CombineType[] regions;


    public void Start()
    {
        heatMap = heatMapGenerator.mapData;
        moistureMap = moistureMapGenerator.mapData;
    }

    public void DrawMap()
    {
        mapData = GenerateMapData();

        if (drawMode == DrawMode.NoiseMap)
        {
            Texture2D texture = TextureGenerator.TextureFromHeightMap(mapData.heightMap);
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            Texture2D texture = TextureGenerator.TextureFromColorMap(mapData.colorMap, mapChunkSize, mapChunkSize);
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }
    }

    MapData GenerateMapData()
    {
        //float heat = 0;
        //float moisture = 0;
        Tile[,] noiseMap = new Tile[mapChunkSize, mapChunkSize];

        //for (int x = 0; x < heatMap.heightMap.GetLength(0); x++)
        //{
        //    for (int y = 0; y < heatMap.heightMap.GetLength(1); y++)
        //    {
        //        heat = heatMap.heightMap[x, y];
        //        moisture = moistureMap.heightMap[x, y];
        //        noiseMap[x, y] = (heat + moisture) / 2f;
        //    }
        //}

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float heat = heatMap.heightMap[x, y].value;
                float moisture = moistureMap.heightMap[x, y].value;
                for (int i = 0; i < regions.Length; i++)
                {
                    if (heat <= regions[i].heat && moisture <= regions[i].moisture)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        return new MapData(noiseMap, colorMap);
        //return new MapData();
    }
}

[CustomEditor(typeof(CombineMap))]
public class CombineMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CombineMap mapGen = (CombineMap)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            mapGen.moistureMapGenerator.DrawMap();
            mapGen.heatMapGenerator.DrawMap();
            mapGen.Start();
            mapGen.DrawMap();
        }
        
    }
}

[System.Serializable]
public struct CombineType
{
    public float moisture;
    public float heat;
    public Color color;
}
