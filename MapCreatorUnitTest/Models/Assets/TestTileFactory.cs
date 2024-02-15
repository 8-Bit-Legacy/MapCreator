using MapCreatorModels.Models.Assets;
using System.Collections.ObjectModel;
using System.Text.Json;
namespace MapCreatorUnitTest.Models.Assets
{
    [TestClass]
    public class TestTileFactory
    {
        TileFactory tileFactory = new TileFactory();

        [TestInitialize]
        public void TestInitialize()
        {
            tileFactory = new();
        }

        [TestMethod]
        public void TestJsonSerialization()
        {
            TileFactory tileFactory = new TileFactory();
            int tileAmount = 12;
            for (int i = 0; i < tileAmount; i++)
            {
                tileFactory.CreateTile("test " + i);
            }

            ObservableCollection<Tile> tiles = tileFactory.GetTiles();

            string jsonString = JsonSerializer.Serialize(tileFactory);

            TileFactory tileFactoryCopy = JsonSerializer.Deserialize<TileFactory>(jsonString);

            for (int i = 0; i < tileAmount; i++)
            {
                string tileName1 = tiles[i].Name;
                string tileName2 = tileFactoryCopy.GetTileById((byte)tiles[i].Id).Name;
                Assert.AreEqual(tileName1, tileName2);
            }
        }
    }
}
