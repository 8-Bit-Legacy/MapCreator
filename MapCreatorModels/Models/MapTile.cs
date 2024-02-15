using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapCreatorModels.Models
{
    public class MapTile
    {
        [JsonIgnore]
        public bool isFlippedHorizontally { get; set; } = false;
        [JsonIgnore]
        public bool isFlippedVertically { get; set; } = false;
        [JsonIgnore]
        public Tile Tile { get; private set; }

        private byte _tileId;
        [JsonInclude]
        private byte Tid
        {
            get { return Tile.Id; }
            set { _tileId = value; }
        }

        public void InitTile(TileFactory tileFactory) { 
            Tile = tileFactory.GetTileById(_tileId);
        }

        public MapTile(Tile tile)
        {
            Tile = tile;
        }

        [JsonConstructor]
        public MapTile() { }
    }
}
