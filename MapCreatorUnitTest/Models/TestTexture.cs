using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using System.Runtime.InteropServices.JavaScript;

namespace MapCreatorUnitTest.Models
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
    }
}