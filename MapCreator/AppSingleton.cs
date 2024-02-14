using MapCreator.Cache;
using MapCreatorModels.DAL;
using MapCreatorModels.Models;
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
        public TileFactory TileFactory { get; init; }
        public ActorFactory ActorFactory { get; init; } = new ActorFactory();
        public WritableBitMapCache TextureCache { get; init; } = new WritableBitMapCache();
        public Map Map { get; set; }

        private AppSingleton()
        {
            TileFactory = Save.LoadTileFactory();
            TileFactory.InitializeList();
            Map = Save.LoadMap();
            Map.InitMap(TileFactory);
        }

        public void SaveState()
        {
            TileFactory.RearrangeList();
            Save.SaveTileFactory(TileFactory);
            Save.SaveMap(Map);
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
