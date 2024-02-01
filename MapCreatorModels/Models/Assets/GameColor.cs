
namespace MapCreatorModels.Models.Assets
{
    public class GameColor
    {
        /// <summary>
        /// Valeur entre 0 et 15 permettant de définir la couleur dans le FPGA
        /// </summary>
        public byte Id { get; }
        private int _colorValue;
        public int ColorValue
        {
            get { return _colorValue; }
            private set
            {
                _colorValue = value;
                Red = (byte)(value >> 16 & 0xff);
                Green = (byte)(value >> 8 & 0xff);
                Blue = (byte)(value & 0xff);
            }
        }
        private byte _red;
        public byte Red
        {
            get { return _red; }
            private set { _red = value; }
        }

        private byte _green;
        public byte Green
        {
            get { return _green; }
            private set { _green = value; }
        }

        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            private set { _blue = value; }
        }

        public GameColor(int ColorValue, byte Id)
        {
            this.ColorValue = ColorValue;
            this.Id = Id;
        }
    }
}
