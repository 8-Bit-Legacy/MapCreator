using MapCreatorModels.Models.Assets;
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
        public MapTile(Tile tile)
        {
            Tile = tile;
        }
    }
}
