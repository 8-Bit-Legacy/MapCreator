
using MapCreatorModels.Models.Assets;
using System.Text.Json.Serialization;

namespace MapCreatorModels.Models
{

    // #------------ <X
    // |
    // |
    // |
    // |
    // |
    // ^
    // Y
    public class Map
    {
        [JsonInclude]
        public int Height { get; init; }

        [JsonInclude]
        public int Width { get; init; }

        [JsonInclude]
        public static MapTile[,] MapTiles { get; private set; } = new MapTile[64, 64];

        public static void SetTile(int x, int y, MapTile tile)
        {
            MapTiles[y, x] = tile;
        }

        [JsonConstructor]
        public Map() { }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}
