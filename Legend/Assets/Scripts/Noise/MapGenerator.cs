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

    public const int mapChunkSize = 250;
    public int noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public bool CombineMap;
    [HideInInspector]
    public MapGenerator heatMap;
    [HideInInspector]
    public MapGenerator moistureMap;

    void Start()
    {
        seed = Random.Range(0, int.MaxValue);
        DrawMap();
    }

    public void DrawMap()
    {
        MapData mapData = GenerateMapData();

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
        float[,] noiseMap = Noise.GeterateNoiseMap(mapChunkSize, mapChunkSize, noiseScale, seed, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
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
    }

    public void DrawTexture(Texture2D texture)
    {

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }


}


public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}

[System.Serializable]
public struct TerrainType
{
    public float height;
    public Color color;
}

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
        if (!mapGen.autoUpdate)
        {
            if (GUILayout.Button("Generate"))
            {
                mapGen.DrawMap();
            }
        }
        if (mapGen.CombineMap)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.TextArea("")
            mapGen.heatMap = (MapGenerator) EditorGUILayout.ObjectField(mapGen.heatMap, typeof(MapGenerator), true);
            EditorGUILayout.EndHorizontal();
        }
    }
}
