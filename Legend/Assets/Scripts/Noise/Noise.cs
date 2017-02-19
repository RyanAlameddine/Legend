using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
    public static Tile[,] GeterateNoiseMap(int mapWidth, int mapHeight, float scale, int seed, int octaves, float persistance, float lacunarity, Vector2 offset, MapGenerator.Mode mode)
    {
        Tile[,] noiseMap = new Tile[mapWidth, mapHeight];

        System.Random rand = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for(int i = 0; i < octaves; i++)
        {
            float offsetX = rand.Next(-100000, 100000) + offset.x;
            float offsetY = rand.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitutde = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {

                    float sampleX = (x + octaveOffsets[i].x - halfWidth) / scale * frequency;
                    float sampleY = (y + octaveOffsets[i].y - halfHeight) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitutde;

                    amplitutde *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y].value = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y].value = Mathf.InverseLerp(-1.3f, .9f, noiseMap[x, y].value);
                if(mode == MapGenerator.Mode.Heat)
                {
                    float heatValue = noiseMap[x, y].value;
                    if (heatValue < Region.ColdestValue)
                        noiseMap[x, y].HeatType = HeatType.Coldest;
                    else if (heatValue < Region.ColderValue)
                        noiseMap[x, y].HeatType = HeatType.Colder;
                    else if (heatValue < Region.ColdValue)
                        noiseMap[x, y].HeatType = HeatType.Cold;
                    else if (heatValue < Region.WarmValue)
                        noiseMap[x, y].HeatType = HeatType.Warm;
                    else if (heatValue < Region.WarmerValue)
                        noiseMap[x, y].HeatType = HeatType.Warmer;
                    else
                        noiseMap[x, y].HeatType = HeatType.Warmest;
                }
            }
        }

        return noiseMap;
    }
}

public struct Tile
{
    public float value;
    public HeatType HeatType;
}