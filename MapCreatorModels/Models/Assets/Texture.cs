using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MapCreatorModels.Models.Assets
{
    /// <summary>
    /// Une texture est un tableau de 128 bytes
    /// Chaques octets représente deux pixels
    /// </summary>
    public class Texture
    {
        public event EventHandler TextureUpdated;

        protected virtual void OnTextureUpdated(EventArgs e)
        {
            TextureUpdated?.Invoke(this, new TextureUpdatedEventArgs() { Texture = this});
        }

        [JsonIgnore]
        public static int Height { get; } = 16;
        [JsonIgnore]
        public static int Width { get; } = 16;

        GameColor[,] _color2DArray;
        /// <summary>
        /// Utiser getColor et setColor pour modifier les couleurs
        /// </summary>
        [JsonIgnore]
        public GameColor[,] Color2DArray
        {
            get { return (GameColor[,])_color2DArray.Clone();}
            private set { }
        }

        public bool IsColor2DArray { get; private set; } = false;

        // Propriete pour le ColorAsSting et le FPGA plus tard
        [JsonIgnore]
        private byte[] ColorByteArray { get { return ColorArray2ColorByteArray(_color2DArray); } }

        // Pour le Json seulement, permet de sauver de l'espace dans le fichier json
        [JsonInclude]
        public string ColorsAsAString
        {
            get { return Convert.ToBase64String(ColorByteArray); }
            set { _color2DArray = ColorByteArray2ColorArray(Convert.FromBase64String(value)); }
        }
        [JsonConstructor]
        public Texture()
        {
            _color2DArray = new GameColor[Height, Width];
            GameColor defaultColor = GameColorList.GetColorById(2);
            FillWithColor(defaultColor);
        }

        public void FillWithColor(GameColor color)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _color2DArray[y, x] = color;
                }
            }
            IsColor2DArray = true;
        }

        public GameColor GetColor(int x, int y)
        {
            return _color2DArray[y, x];
        }

        public byte GetColorId(int x, int y)
        {
            return _color2DArray[y, x].Id;
        }

        public void SetColor(byte colorId, int x, int y)
        {
            _color2DArray[y, x] = GameColorList.GetColorById(colorId);
            IsColor2DArray = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetByteArrayColorIndex(int x, int y)
        {
            return (y * Height + x) / 2;
        }

        /// <summary>
        /// Envois les index du color array selon l'index de l'array de byte
        /// Comme l'array de byte contient 2 pixels par byte x1 est la valeur du pixel 1 et x2 est la valeur du pixel 2
        /// </summary>
        /// <param name="index">Index Array 1D</param>
        /// <param name="x1">Index X du pixel 1</param>
        /// <param name="x2">Index X du pixel 2</param>
        /// <param name="y">Index Y des pixel</param>
        private void GetColorArrayIndex(int index, out int x1, out int x2, out int y)
        {
            int tempValue = index * 2;
            int xtemp = tempValue % Width;
            x1 = xtemp;
            x2 = xtemp + 1;
            y = (tempValue - xtemp) / Height;
        }

        private byte[] ColorArray2ColorByteArray(GameColor[,] color2DArray)
        {
            byte[] colorByteArray = new byte[color2DArray.Length / 2];
            for (int y = 0; y < color2DArray.GetLength(0); y++)
            {
                for (int x = 0; x < color2DArray.GetLength(1); x++)
                {
                    int index = GetByteArrayColorIndex(x, y);
                    if (x % 2 == 0)
                        colorByteArray[index] = colorByteArray[index] |= (byte)(color2DArray[y, x].Id << 4);
                    else
                        colorByteArray[index] = colorByteArray[index] |= color2DArray[y, x].Id;
                }
            }
            return colorByteArray;
        }

        private GameColor[,] ColorByteArray2ColorArray(byte[] colorByteArray)
        {
            GameColor[,] colors = new GameColor[Height, Width];
            for (int i = 0; i < colorByteArray.Length; i++)
            {
                byte left = (byte)((colorByteArray[i] & 0xF0) >> 4);
                byte right = (byte)(colorByteArray[i] & 0x0F);
                GetColorArrayIndex(i, out int x1Value, out int x2Value, out int yValue);

                colors[yValue, x1Value] = GameColorList.GetColorById(left);
                colors[yValue, x2Value] = GameColorList.GetColorById(right);
            }
            return colors;
        }

        /// <summary>
        /// Copies Gamecolor array into the current texture
        /// </summary>
        /// <param name="texture"></param>
        public void UpdateTexture(Texture texture)
        {
            for (int y = 0; y < _color2DArray.GetLength(0); y++)
            {
                for (int x = 0; x < _color2DArray.GetLength(1); x++)
                {
                    this._color2DArray[y, x] = texture._color2DArray[y, x];
                }
            }
            OnTextureUpdated(null);
        }

        public void FillTextureWithColor(GameColor color) {
            for (int y = 0; y < _color2DArray.GetLength(0); y++)
            {
                for (int x = 0; x < _color2DArray.GetLength(1); x++)
                {
                    this._color2DArray[y, x] = color;
                }
            }
            OnTextureUpdated(null);
        }

        public object Clone()
        {
            Texture texture = new Texture();

            //Copy des references dans un nouvel array
            for (int y = 0; y < _color2DArray.GetLength(0); y++)
            {
                for (int x = 0; x < _color2DArray.GetLength(1); x++)
                {
                    texture._color2DArray[y, x] = _color2DArray[y, x];
                }
            }
            return texture;
        }
    }
}
