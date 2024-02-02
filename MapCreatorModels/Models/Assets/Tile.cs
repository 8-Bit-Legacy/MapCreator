using System.Text.Json.Serialization;

namespace MapCreatorModels.Models.Assets
{
    public class Tile
    {
        [JsonInclude]
        public byte? Id { get; internal set; }

        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        bool isColliding { get; set; } = false;

        //Une texture peut etre modifiee, mais pas assigne
        [JsonInclude]
        public Texture Texture { get; private set; }
        [JsonConstructor]
        private Tile() { }
        internal Tile(byte? id, string name)
        {
            Id = id;
            Name = name;
            Texture = new Texture();
        }
        internal Tile(byte? id, string name, Texture texture)
        {
            Id = id;
            Name = name;
            Texture = texture;
        }
    }
}
