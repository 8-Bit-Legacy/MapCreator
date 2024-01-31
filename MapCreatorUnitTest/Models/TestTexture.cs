using MapCreatorModels.Models;
using System.Runtime.InteropServices.JavaScript;

namespace MapCreatorUnitTest.Models
{
    [TestClass]
    public class TextureUnitTest
    {
        Color[,] colorReference = new Color[16, 16];
        Texture texture = new();


        [TestInitialize]
        public void TestInitialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < colorReference.GetLength(0); i++)
            {
                for (int j = 0; j < colorReference.GetLength(1); j++)
                {
                    colorReference[j, i] = Color.GetColorById((byte)rnd.Next(0, 16));
                    texture.SetColor(colorReference[j, i], i, j);
                }
            }
        }

        [TestMethod]
        public void TestColorAsString()
        {

        }

        [TestMethod]
        public void TestGetColorsAsByteArray()
        {

        }

        [TestMethod]
        public void TestGetColors()
        {
            //Tester l'obtention de la référence de la matrice de couleur
            for (int i = 0; i < colorReference.GetLength(0); i++)
            {
                for (int j = 0; j < colorReference.GetLength(1); j++)
                {
                   Assert.AreEqual(colorReference[j, i], texture.GetColors()[j, i]);
                   Assert.AreEqual(colorReference[j, i], texture.GetColor(i,j)); 
                   Assert.AreEqual(colorReference[j, i].Id, texture.GetColorValue(i,j)); 
                }
            }

            //Tester l'obtention du byte de la matrice de couleur
        }
    }
}