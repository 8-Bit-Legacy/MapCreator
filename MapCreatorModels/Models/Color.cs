
namespace MapCreatorModels.Models
{
    public class Color
    {
        /// <summary>
        /// Valeur entre 0 et 15 permettant de définir la couleur dans le FPGA
        /// </summary>
        public byte Id { get; }
        private int _colorValue;
        public int ColorValue { 
            get { return _colorValue; }
            private set {
                _colorValue = value;
                Red = (byte)((value >> 16) & 0xff);
                Green = (byte)((value >> 8) & 0xff);
                Blue = (byte)(value & 0xff);
            }
        }
        private byte _red;
        public byte Red {
            get { return _red; }
            private set { _red = value; }
        }

        private byte _green;
        public byte Green { 
            get { return _green; }
            private set { _green = value; }
        }

        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            private set { _blue = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColorValue">Color Value in HexaDecimal</param>
        /// <param name="Id"></param>
        public Color(int ColorValue, byte Id)
        {
            this.ColorValue = ColorValue;
            this.Id = Id;
        }

        //TODO : Change the colors. IMPORTANT, THE COLOR ID MUST ALWAY BE IT'S INDEX IN THE ARRAY
        private static readonly Color[] _Colors = [
            new Color(0x000000,0),
            new Color(0x0000AA,1),
            new Color(0x00AA00,2),  
            new Color(0x00AAAA,3),
            new Color(0xAA0000,4),
            new Color(0xAA00AA,5),
            new Color(0xAA5500,6),
            new Color(0xAAAAAA,7),
            new Color(0x555555,8),
            new Color(0x5555FF,9),
            new Color(0x55FF55,10),
            new Color(0x55FFFF,11),
            new Color(0xFF5555,12),
            new Color(0xFF55FF,13),
            new Color(0xFFFF55,14),
            new Color(0xFFFFFF,15)
        ];

        public static Color[] GetColors()
        {
            return _Colors;
        }

        public static Color GetColorById(byte id)
        {
            return _Colors[id];
        }
    }
}
