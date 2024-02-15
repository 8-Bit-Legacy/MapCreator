﻿using System.Text.Json;
using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;

namespace MapCreatorModels.DAL
{
    public static class Save
    {
        private const string tileFileName = "tiles.json";
        private const string mapFileName = "map.json";
        public static void SaveTiles(List<Tile> tiles)
        {
            string jsonString = JsonSerializer.Serialize(tiles);
            File.WriteAllText(tileFileName, jsonString);
        }

        public static void SaveMap(Map map)
        {
            string jsonString = JsonSerializer.Serialize(map);
            File.WriteAllText(tileFileName, jsonString);
        }

        public static Tile[] LoadTiles()
        {
            string jsonString = File.ReadAllText(tileFileName);
            
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Tile[] tiles = JsonSerializer.Deserialize<Tile[]>(jsonString);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (tiles is null)
                throw new Exception("Error while loading tiles");

            return tiles;
        }

        public static Map LoadMap()
        {
            string jsonString = File.ReadAllText(mapFileName);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Map map  = JsonSerializer.Deserialize<Map>(jsonString);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return map;
        }

        public static Texture LoadTexture()
        {
            string jsonString = File.ReadAllText("texture.json");
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Texture texture = JsonSerializer.Deserialize<Texture>(jsonString);
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return texture;
        }

        public static void SaveTexture(Texture texture)
        {
            string jsonString = JsonSerializer.Serialize(texture);
            File.WriteAllText("texture.json", jsonString);
        }
    }
}
