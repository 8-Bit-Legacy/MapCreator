using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets.AssetsFactory
{
    public class TileFactory : AssetFactory<Tile>
    {
        [JsonConstructor]
        public TileFactory() { }
        public override Tile CopyAsset(Tile asset)
        {
            Tile tileCopy = new(AssetCount++, asset.Name + " Copy", (Texture)asset.Texture.Clone());
            _assetDictionnary.Add((byte)tileCopy.Id, tileCopy);
            _assetList.Add(tileCopy);
            return tileCopy;
        }

        public override Tile CopyAsset(byte asset)
        {
            return CopyAsset(GetTileById(asset));
        }

        public override Tile CreateAsset(string name)
        {
            Tile newTile = new(AssetCount++, name);
            _assetDictionnary.Add((byte)newTile.Id, newTile);
            _assetList.Add(newTile);
            return newTile;
        }

        public Tile GetTileById(byte id)
        {
            return (Tile)_assetDictionnary[id];
        }
        public override void RearrangeList()
        {
            for (int i = 0; i < _assetList.Count; i++)
                _assetList[i].Id = (byte)i;
            AssetCount = (byte)_assetList.Count;
        }
    }
}
