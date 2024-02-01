using MapCreatorModels.Models.Assets;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace MapCreatorUnitTest.Models.Assets
{
    [TestClass]
    public class TextureUnitTest
    {
        GameColor[,] colorReference = new GameColor[16, 16];
        Texture texture = new();

        [TestInitialize]
        public void TestInitialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < colorReference.GetLength(0); i++)
            {
                for (int j = 0; j < colorReference.GetLength(1); j++)
                {
                    colorReference[j, i] = GameColorList.GetColorById((byte)rnd.Next(0, 16));
                    texture.SetColor(colorReference[j, i].Id, i, j);
                }
            }
        }

        [TestMethod]
        public void TestGetColors()
        {
            string color = texture.ColorsAsAString;

            Texture texture2 = new();
            texture2.ColorsAsAString = color;

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    Assert.AreEqual(texture.Color2DArray[y, x], texture2.Color2DArray[y, x]);
                }
            }

        }

        [TestMethod]
        public void TestClone()
        {
            Texture texture2 = (Texture)texture.Clone();

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    Assert.AreEqual(texture.Color2DArray[y, x], texture2.Color2DArray[y, x]);
                }
            }
        }

        [TestMethod]
        public void TestJsonSerialization()
        {
            string jsonString = JsonSerializer.Serialize(texture);
            Texture texture2 = JsonSerializer.Deserialize<Texture>(jsonString);

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    Assert.AreEqual(texture.Color2DArray[y, x], texture2.Color2DArray[y, x]);
                }
            }
        }
    }
}