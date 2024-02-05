using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets.AssetsFactory
{
    public class ActorFactory : AssetFactory<Actor>
    {
        public override Actor CopyAsset(Actor asset)
        {
            Actor actorCopy = new(AssetCount++, asset.Name + " Copy", (Texture)asset.Texture.Clone());
            _assetDictionnary.Add(actorCopy.Id, actorCopy);
            _assetList.Add(actorCopy);
            return actorCopy;
        }

        public override Actor CopyAsset(byte asset)
        {
            return CopyAsset(GetActorById(asset));
        }

        public override Actor CreateAsset(string name)
        {
            Actor newActor = new(AssetCount++, name);
            _assetDictionnary.Add((byte)newActor.Id, newActor);
            _assetList.Add(newActor);
            return newActor;
        }

        public Actor GetActorById(byte id)
        {
            return (Actor)_assetDictionnary[id];
        }

        public override void RearrangeList()
        {
            for (int i = 0; i < _assetList.Count; i++)
                _assetList[i].Id = (byte)i;
            AssetCount = (byte)_assetList.Count;
        }
    }
}
