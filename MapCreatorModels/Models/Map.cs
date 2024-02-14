
using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;
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
        public event EventHandler OnTileRemoved;

        public event EventHandler TextureUpdated;

        protected virtual void OnTextureUpdated(EventArgs e)
        {
            TextureUpdated?.Invoke(this, e);
        }
        void TextureUpdatedReceiver(object sender, EventArgs e)
        {
            OnTextureUpdated(e);
        }

        void AssetRemovedUpdate(object sender, EventArgs e)
        {
            AssetRemovedEventArgs assetEvent = (AssetRemovedEventArgs)e;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    MapTile maptile = MapTiles[y * Height + x];
                    if (maptile != null && maptile.Tile == assetEvent.Asset)
                    {
                        OnTileRemoved?.Invoke(this, new TileRemovedEventArgs() { Row = y, Col = x });
                        MapTiles[y * Height + x] = null;
                    }
                }
            }
        }
        //Permet de verifier si on écoute déja l'event
        [JsonIgnore]
        private Dictionary<Texture, bool> _mapTiles = new Dictionary<Texture, bool>();

        [JsonInclude]
        public static int Height { get; } = 64;

        [JsonInclude]
        public static int Width { get; } = 64;

        [JsonInclude]
        private MapTile[] MapTiles { get; set; }
        public MapTile getMapTile(int x, int y)
        {
            return MapTiles[y * Height + x];
        }
        public void SetTile(int x, int y, MapTile mapTile)
        {
            MapTiles[y * Height + x] = mapTile;
            AddListener(mapTile);
        }
        public void InitMap(TileFactory tileFactory)
        {
            tileFactory.AssetRemoved += AssetRemovedUpdate;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    MapTile mapTile = MapTiles[y * Height + x];
                    if (mapTile != null)
                    {
                        mapTile?.InitTile(tileFactory);
                        AddListener(mapTile);
                    }
                }
            }
        }

        private void TileFactory_AssetRemoved(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddListener(MapTile mapTile)
        {
            if (!_mapTiles.ContainsKey(mapTile.Tile.Texture))
            {
                mapTile.Tile.Texture.TextureUpdated += TextureUpdatedReceiver;
                _mapTiles.Add(mapTile.Tile.Texture, true);
            }
        }

        public Map(int Height, int Width)
        {
            MapTiles = new MapTile[Height * Width];
        }

        [JsonConstructor]
        public Map()
        {

        }
    }
}
