using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;

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

        [JsonIgnore]
        private ObservableCollection<Tile> tileList = new ObservableCollection<Tile>();

        public Tile CreateTile(string name)
        {
            Tile tile = new(TileCount++, name);
            _tiles.Add((byte)tile.Id, tile);
            tileList.Add(tile);
            return tile;
        }

        public Tile GetTileById(byte id)
        {
            return _tiles[id];
        }
        public bool DeleteTile(byte id)
        {   
            tileList.Remove(_tiles[id]);
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
            tileList.Add(tile);
            return tileCopy;
        }

        public ObservableCollection<Tile> GetTiles()
        {
            return tileList;
        }

        public void InitializeTileList()
        {
            foreach (var tile in _tiles)
            {
                tileList.Add(tile.Value);
            }
        }
    }
}
