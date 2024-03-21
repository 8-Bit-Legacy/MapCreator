using System.Text.Json.Serialization;

namespace MapCreatorModels.Models.Assets
{
    public class Tile : Asset
    {
        [JsonInclude]
        public bool isCollision { get; set; } = false;
        [JsonConstructor]
        private Tile() : base() { }
        internal Tile(byte id, string name) : base(id, name) { }
        internal Tile(byte id, string name, Texture texture) : base(id, name, texture) { }
    }
}
