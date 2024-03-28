using System.Drawing;

namespace MapCreatorModels.Models.Assets
{
    public class GameColor
    {
        public Bitmap Bitmap { get; set; }

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

        public int RBG { get {
                int temp = 0;
                temp |= Red << 16;
                temp |= Blue << 8;
                temp |= Green;
                return temp;
            } }
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

        public string Name { get; init; }

        public GameColor(int ColorValue, byte Id)
        {
            this.ColorValue = ColorValue;
            Name = Convert.ToHexString([Red, Green, Blue]);
            this.Id = Id;
            Bitmap = new Bitmap(1, 1);
            Bitmap.SetPixel(0, 0, System.Drawing.Color.FromArgb(255, Red, Green, Blue));
        }
    }
}
