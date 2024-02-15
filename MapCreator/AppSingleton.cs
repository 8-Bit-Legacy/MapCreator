using MapCreator.Cache;
using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreator
{
    internal class AppSingleton
    {
        private static AppSingleton instance;


        public TileFactory TileFactory { get; init; } = new TileFactory();
        public WritableBitMapCache TextureCache { get; init; } = new WritableBitMapCache();

        private AppSingleton()
        {
            TileFactory.InitializeTileList();
            TileFactory.CreateTile("Grass");
            TileFactory.CreateTile("Dirt");
            TileFactory.CreateTile("Water");
            TileFactory.CreateTile("Sand");
            TileFactory.CreateTile("Stone");
            TileFactory.CreateTile("Wood");
        }

        public static AppSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppSingleton();
                }
                return instance;
            }
        }
    }
}
