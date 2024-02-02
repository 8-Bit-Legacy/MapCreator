using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorUnitTest.Models.Assets
{
    [TestClass]
    public class TestColor
    {
        GameColor[] colorReference;


        [TestInitialize]
        public void TestInitialize()
        {
            colorReference = GameColorList.GetColors();
        }
        [TestMethod]
        public void TestGetColorById()
        {
            for (int i = 0; i < colorReference.Length; i++)
            {
                Assert.AreEqual(colorReference[i], GameColorList.GetColorById((byte)i));
            }
        }
    }
}
