using System;
using System.Drawing;
using System.Text.Json.Serialization;


namespace MapCreatorModels.Models
{
    /// <summary>
    /// Une texture est un tableau de 128 bytes
    /// Chaques octets représente deux pixels
    /// </summary>
    public class Texture
    {
        //NE PAS CHAGER LA VALEUR DE L'ARRAY DIRECTEMENT PASSER PAR LES METHODES
        [JsonIgnore]
        private Color[,] _colors = new Color[16,16];

        //NE PAS CHAGER LA VALEUR DE L'ARRAY DIRECTEMENT PASSER PAR LES METHODES
        [JsonIgnore]
        private byte[] _byteArrayColors = new byte[128];

        // Permet de convertir un tableau 2d de byte en un tableau de string ou chaque caractère représente 2 pixels
        // sinon le json serait trop gros
        [JsonInclude]
        public string ColorsAsAString
        {
            get { return Convert.ToBase64String(_byteArrayColors); }
            set
            {
                _byteArrayColors = Convert.FromBase64String(value);
                _colors = ColorByteArrayToColorArray(_byteArrayColors);
            }
        }

        public byte[] GetColorsAsByteArray()
        {
            return _byteArrayColors;
        }

        public Color[,] GetColors()
        {
            return _colors;
        }

        public Color GetColor(int x, int y)
        {
            return _colors[y, x];
        }
        //For testing purpose only
        public int GetColorValue(int x, int y)
        {
            int index = GetByteArrayColorIndex(x, y);
            if (x % 2 == 1)
                return (byte)(_byteArrayColors[index] & 0x0F);
            else
                return (byte)((_byteArrayColors[index] & 0xF0) >> 4);

        }

        public void SetColor(Color color, int x, int y)
        {
            _colors[y, x] = color;
            SetByteArrayColor(color.Id, x, y);
        }

        public void SetColor(byte colorId, int x, int y)
        {
            _colors[y, x] = Color.GetColorById(colorId);
            SetByteArrayColor(colorId, x, y);
        }

        private void SetByteArrayColor(byte colorId, int x, int y)
        {
            int index = y * ((_colors.GetLength(0) - 1) / 2) + x / 2;
            //regarder si x est impair ou pair
            //Si x est impair alors on doit changer les 4 derniers bits
            if (x % 2 == 1)
                _byteArrayColors[index] = (byte)((_byteArrayColors[index] & 0xF0) | colorId);
            else
                _byteArrayColors[index] = (byte)((_byteArrayColors[index] & 0x0F) | (colorId << 4));
        }
        /// <summary>
        /// Obtenir la valeur de l'index du tableau de byte
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        private int GetByteArrayColorIndex(int x, int y) {
            return y * ((_colors.GetLength(0) - 1) / 2) + x / 2;
        }

        private static Color[,] ColorByteArrayToColorArray(byte[] colorArray) {
            Color[,] colors = new Color[16, 16];
            for (int i = 0; i < colorArray.Length; i++)
            {
                int y = i % 16;
                int x1 = (i % 8) * 2;
                int x2 = x1 + 1;
                // Left byte element
                colors[y, x1] = Color.GetColorById((byte)(colorArray[i] & 0x0F));
                //Right byte element
                colors[y, x2] = Color.GetColorById((byte)((colorArray[i] & 0xF0) >> 4));
            }
            return colors;
        }
         
        public Texture()
        {
            
        }
    }
}
