using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class CombineMap : MonoBehaviour
{

    public MapGenerator heatMapGenerator;
    public MapGenerator moistureMapGenerator;

    MapData heatMap;
    MapData moistureMap;

    public MapData mapData;

    public Renderer textureRenderer;

    public bool generate;

    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode drawMode;

    public const int mapChunkSize = 250;

    public void OnValidate()
    {
        moistureMapGenerator.DrawMap();
        heatMapGenerator.DrawMap();
        Start();
        DrawMap();
        if (generate) generate = false;
    }


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
        textureRenderer.transform.localScale = new Vector3(15, 1, 15);
    }

    MapData GenerateMapData()
    {
        Tile[,] noiseMap = new Tile[mapChunkSize, mapChunkSize];

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                switch (Region.BiomeTable[(int)heatMap.heightMap[x, y].HeatType, (int)moistureMap.heightMap[x, y].MoistureType])
                {
                    case BiomeType.Ice:
                        colorMap[y * mapChunkSize + x] = Region.Ice;
                        break;
                    case BiomeType.ColdForest:
                        colorMap[y * mapChunkSize + x] = Region.ColdForest;
                        break;
                    case BiomeType.Desert:
                        colorMap[y * mapChunkSize + x] = Region.Desert;
                        break;
                    case BiomeType.Grassland:
                        colorMap[y * mapChunkSize + x] = Region.Grassland;
                        break;
                    case BiomeType.SeasonalForest:
                        colorMap[y * mapChunkSize + x] = Region.SeasonalForest;
                        break;
                    case BiomeType.Tundra:
                        colorMap[y * mapChunkSize + x] = Region.Tundra;
                        break;
                    case BiomeType.Savanna:
                        colorMap[y * mapChunkSize + x] = Region.Savanna;
                        break;
                    case BiomeType.TemperateRainforest:
                        colorMap[y * mapChunkSize + x] = Region.TemperateRainforest;
                        break;
                    case BiomeType.TropicalRainforest:
                        colorMap[y * mapChunkSize + x] = Region.TropicalRainforest;
                        break;
                    case BiomeType.Woodland:
                        colorMap[y * mapChunkSize + x] = Region.Woodland;
                        break;
                }
            }
        }

        return new MapData(noiseMap, colorMap);
    }
}
