using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets
{
    public class GameColorList
    {
        private static readonly GameColor[] _Colors = [
            new GameColor(0xFFFFFF, 0),
            new GameColor(0x0000AA, 1),
            new GameColor(0x86c06c, 2),
            new GameColor(0x00AAAA, 3),
            new GameColor(0xAA0000, 4),
            new GameColor(0x8308e4, 5),
            new GameColor(0xc2b28b, 6),
            new GameColor(0x7f7e7a, 7),
            new GameColor(0x484848, 8),
            new GameColor(0x5555FF, 9),
            new GameColor(0xe0f8cf, 10),
            new GameColor(0xfce0a8, 11),
            new GameColor(0xFF5555, 12),
            new GameColor(0x000000, 13),
            new GameColor(0xFFFF55, 14),
            new GameColor(0x306850, 15)
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
