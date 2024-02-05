using System.Text.Json;
using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;

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
    }
}
