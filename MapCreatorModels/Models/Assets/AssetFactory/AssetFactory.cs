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

namespace MapCreatorModels.Models.Assets.AssetsFactory
{
    public abstract class AssetFactory<T>
    {
        [JsonConstructor]
        public AssetFactory() { }
        [JsonInclude]
        public byte AssetCount { get; protected set; }

        [JsonInclude]
        protected Dictionary<byte, Asset> _assetDictionnary { get; set; } = [];

        [JsonIgnore]
        protected ObservableCollection<Asset> _assetList = new ObservableCollection<Asset>();

        public abstract void RearrangeList();
        public abstract T CreateAsset(string name);

        public abstract T CopyAsset(T asset);
        public abstract T CopyAsset(byte asset);
        public Asset GetAssetById(byte id) {
            return _assetDictionnary[id];
        }

        public bool DeleteAsset(byte id)
        {
            _assetList.Remove(_assetDictionnary[id]);
            return _assetDictionnary.Remove(id);
        }

        public ObservableCollection<Asset> GetObservableCollection()
        {
            return _assetList;
        }

        public void InitializeList()
        {
            foreach (var asset in _assetDictionnary)
            {
                _assetList.Add(asset.Value);
            }
        }
    }
}
