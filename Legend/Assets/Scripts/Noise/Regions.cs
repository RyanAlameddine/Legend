using UnityEngine;
using System.Collections;


public enum BiomeType
{
    Desert,
    Savanna,
    TropicalRainforest,
    Grassland,
    Woodland,
    SeasonalForest,
    TemperateRainforest,
    ColdForest,
    Tundra,
    Ice
}

public class Region {
    public const float DryerValue = 0.27f;
    public const float DryValue = 0.4f;
    public const float WetValue = 0.6f;
    public const float WetterValue = 0.8f;
    public const float WettestValue = 0.9f;

    public static Color Dryest = new Color(0, 0, 0);
    public static Color Dryer = new Color(0, 0, .2f);
    public static Color Dry = new Color(0, 0, .4f);
    public static Color Wet = new Color(0, 0, .6f);
    public static Color Wetter = new Color(0, 0, .8f);
    public static Color Wettest = new Color(0, 0, 1f);

    public const float ColdestValue = 0.05f;
    public const float ColderValue = 0.18f;
    public const float ColdValue = 0.4f;
    public const float WarmValue = 0.6f;
    public const float WarmerValue = 0.8f;

    public static Color Coldest = new Color(0, 0, 1f);
    public static Color Colder = new Color(0, 0, .7f);
    public static Color Cold = new Color(.1f, 0, .3f);
    public static Color Warm = new Color(.3f, 0, .1f);
    public static Color Warmer = new Color(.7f, 0, 0);
    public static Color Hot = new Color(1f, 0, 0);

    public static BiomeType[,] BiomeTable = new BiomeType[6, 6] {
    //COLDEST        //COLDER          //COLD                //HOT                          //HOTTER                       //HOTTEST
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.Grassland,  BiomeType.Desert,              BiomeType.Desert,              BiomeType.Desert },              //DRYEST
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.Grassland,  BiomeType.Desert,              BiomeType.Desert,              BiomeType.Desert },              //DRYER
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.Woodland,   BiomeType.Woodland,            BiomeType.Savanna,             BiomeType.Savanna },             //DRY
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.ColdForest, BiomeType.Woodland,            BiomeType.Savanna,             BiomeType.Savanna },             //WET
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.ColdForest, BiomeType.SeasonalForest,      BiomeType.TropicalRainforest,  BiomeType.TropicalRainforest },  //WETTER
    { BiomeType.Ice, BiomeType.Tundra, BiomeType.ColdForest, BiomeType.TemperateRainforest, BiomeType.TropicalRainforest,  BiomeType.TropicalRainforest }   //WETTEST
  };

    public static Color Ice = Color.white;
    public static Color Desert = new Color(238 / 255f, 218 / 255f, 130 / 255f, 1);
    public static Color Savanna = new Color(177 / 255f, 209 / 255f, 110 / 255f, 1);
    public static Color TropicalRainforest = new Color(66 / 255f, 123 / 255f, 25 / 255f, 1);
    public static Color Tundra = new Color(96 / 255f, 131 / 255f, 112 / 255f, 1);
    public static Color TemperateRainforest = new Color(29 / 255f, 73 / 255f, 40 / 255f, 1);
    public static Color Grassland = new Color(164 / 255f, 225 / 255f, 99 / 255f, 1);
    public static Color SeasonalForest = new Color(73 / 255f, 100 / 255f, 35 / 255f, 1);
    public static Color ColdForest = new Color(95 / 255f, 115 / 255f, 62 / 255f, 1);
    public static Color Woodland = new Color(139 / 255f, 175 / 255f, 90 / 255f, 1);

}

public enum MoistureType
{
    Wettest,
    Wetter,
    Wet,
    Dry,
    Dryer,
    Dryest
}

public enum HeatType
{
    Coldest,
    Colder,
    Cold,
    Warm,
    Warmer,
    Warmest
}
