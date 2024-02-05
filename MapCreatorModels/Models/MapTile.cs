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
        // Pas encore sure si on est mieux de faire des rotations ou des flips
        [JsonIgnore]
        public bool isFlippedHorizontally { get; set; } = false;
        [JsonIgnore]
        public bool isFlippedVertically { get; set; } = false;
        [JsonIgnore]
        Tile tile { get; set; }
        public MapTile(Tile tile)
        {
        }
    }
}
