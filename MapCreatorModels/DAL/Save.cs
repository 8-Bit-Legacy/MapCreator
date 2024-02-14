using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;
using System.Text.Json;

namespace MapCreatorModels.DAL
{
    public static class Save
    {
        private const string tileFactoryFileName = "tileFactory.json";
        private const string mapFileName = "map.json";

        public static void SaveTileFactory(TileFactory tileFactory)
        {
            string json = JsonSerializer.Serialize(tileFactory);
            System.IO.File.WriteAllText(tileFactoryFileName, json);
        }

        public static TileFactory LoadTileFactory()
        {
            if (File.Exists(tileFactoryFileName))
            {
                string json = System.IO.File.ReadAllText(tileFactoryFileName);
                return JsonSerializer.Deserialize<TileFactory>(json);
            }
            return new TileFactory();
        }

        public static void SaveMap(Map map)
        {
            string json = JsonSerializer.Serialize(map);
            System.IO.File.WriteAllText(mapFileName, json);
        }

        public static Map LoadMap()
        {
            if (File.Exists(mapFileName))
            {
                string json = System.IO.File.ReadAllText(mapFileName);
                return JsonSerializer.Deserialize<Map>(json);
            }
            return new Map(64,64);
        }
    }
}
