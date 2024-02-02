using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets
{
    public class TileFactory
    {
        [JsonConstructor]
        public TileFactory() { }
        [JsonInclude]
        public byte TileCount { get; private set; }
        [JsonInclude]
        private Dictionary<byte, Tile> _tiles { get; set; } = [];

        public Tile CreateTile(string name)
        {
            Tile tile = new(TileCount++, name);
            _tiles.Add((byte)tile.Id, tile);
            return tile;
        }

        public Tile GetTileById(byte id)
        {
            return _tiles[id];
        }
        public bool DeleteTile(byte id)
        {
            return _tiles.Remove(id);
        }
        /// <summary>
        /// Copie la Tile et l'ajoute a la liste
        /// </summary>
        /// <returns></returns>
        public Tile CopyTile(Tile tile)
        {
            Tile tileCopy = new(TileCount++, tile.Name + " Copy", tile.Texture);
            _tiles.Add((byte)tile.Id, tile);
            return tileCopy;
        }

        public Tile[] GetTiles()
        {
            return _tiles.Values.ToArray();
        }
    }
}
