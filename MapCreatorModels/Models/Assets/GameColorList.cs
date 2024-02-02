using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets
{
    public class GameColorList
    {
        //TODO : Change the colors. IMPORTANT, THE COLOR ID MUST ALWAY BE IT'S INDEX IN THE ARRAY
        private static readonly GameColor[] _Colors = [
            new GameColor(0x000000, 0),
            new GameColor(0x0000AA, 1),
            new GameColor(0x00AA00, 2),
            new GameColor(0x00AAAA, 3),
            new GameColor(0xAA0000, 4),
            new GameColor(0xAA00AA, 5),
            new GameColor(0xAA5500, 6),
            new GameColor(0xAAAAAA, 7),
            new GameColor(0x555555, 8),
            new GameColor(0x5555FF, 9),
            new GameColor(0x55FF55, 10),
            new GameColor(0x55FFFF, 11),
            new GameColor(0xFF5555, 12),
            new GameColor(0xFF55FF, 13),
            new GameColor(0xFFFF55, 14),
            new GameColor(0xFFFFFF, 15)
        ];

        public static GameColor[] GetColors()
        {
            return _Colors;
        }

        public static GameColor GetColorById(byte id)
        {
            return _Colors[id];
        }
    }
}
