using MapCreator.Cache;
using MapCreatorModels.Models.Assets.AssetsFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreator
{
    internal class AppSingleton
    {
        private static AppSingleton _instance;
        public TileFactory TileFactory { get; init; } = new TileFactory();
        public ActorFactory ActorFactory { get; init; } = new ActorFactory();
        public WritableBitMapCache TextureCache { get; init; } = new WritableBitMapCache();

        private AppSingleton()
        {
            TileFactory.InitializeList();
            TileFactory.CreateAsset("Grass");
            TileFactory.CreateAsset("Dirt");
            TileFactory.CreateAsset("Water");
            TileFactory.CreateAsset("Sand");
            TileFactory.CreateAsset("Stone");
            TileFactory.CreateAsset("Wood");
        }

        public static AppSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSingleton();
                }
                return _instance;
            }
        }
    }
}
