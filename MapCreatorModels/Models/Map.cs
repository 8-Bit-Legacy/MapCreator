
using MapCreatorModels.Models.Assets;
using System.Runtime.CompilerServices;
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
        public event EventHandler TextureUpdated;

        protected virtual void OnTextureUpdated(EventArgs e)
        {
            TextureUpdated?.Invoke(this, e);
        }
        void TextureUpdatedReceiver(object sender, EventArgs e)
        {
            OnTextureUpdated(e);
        }
        //Permet de verifier si on écoute déja l'event
        [JsonIgnore]
        private Dictionary<Texture, bool> _mapTiles = new Dictionary<Texture, bool>();

        [JsonInclude]
        public static  int Height { get; } = 64;

        [JsonInclude]
        public static int Width { get; } = 64;

        [JsonInclude]
        public MapTile[,] MapTiles { get; private set; } = new MapTile[64, 64];

        public void SetTile(int x, int y, MapTile mapTile)
        {
            MapTiles[y, x] = mapTile;
            if (!_mapTiles.ContainsKey(mapTile.Tile.Texture)) {
                mapTile.Tile.Texture.TextureUpdated += TextureUpdatedReceiver;
                _mapTiles.Add(mapTile.Tile.Texture, true);
            }
        }

        [JsonConstructor]
        public Map() { }
    }
}
