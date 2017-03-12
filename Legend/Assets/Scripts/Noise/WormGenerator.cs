using UnityEngine;
using System.Collections;
using UnityEditor;

public class WormGenerator : MonoBehaviour
{
    public int seed;

    public Transform playerTransform;

    public Renderer textureRenderer;

    public const int mapChunkSize = 50;
    public int noiseScale;

    public float maxNoise;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    public bool autoUpdate;

    //[SerializeField]
    //CombineMap map;

    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode drawMode;

    //public TerrainType[] regions;

    public MapData mapData;

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
            tempMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
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
        Tile[,] noiseMap = Noise.GeterateNoiseMap(mapChunkSize, mapChunkSize, noiseScale, seed, octaves, persistance, lacunarity, offset, MapGenerator.Mode.Heat, true);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y].value;
                colorMap[y * mapChunkSize + x] = currentHeight < maxNoise ? Color.blue : Color.clear;
            }
        }

        return new MapData(noiseMap, colorMap);
    }

    public void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            lacunarity = 0;
        }
        //map.Start();
        //map.DrawMap();
    }

    public void DrawTexture(Texture2D texture)
    {

        textureRenderer.material.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(5, 1, 5);
    }


}

//[System.Serializable]
//public struct TerrainType
//{
//    public float height;
//    public Color color;
//}

[CustomEditor(typeof(WormGenerator))]
public class WormGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WormGenerator mapGen = (WormGenerator)target;

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
