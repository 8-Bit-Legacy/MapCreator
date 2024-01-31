using System.Text.Json.Serialization;

namespace MapCreatorModels.Models
{
    public class Tile
    {
        [JsonInclude]
        byte Id;
        [JsonInclude]
        string TextureName { get; set; }
        [JsonInclude]
        bool isColliding { get; set; }
        [JsonInclude]
        public Texture Texture { get; private set; }

        List<Tile> Tiles = new List<Tile>();
    }
}
