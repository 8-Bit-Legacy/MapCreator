using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using MapCreatorModels.Models.Assets;

namespace MapCreator.Converter
{
    [ValueConversion(typeof(Texture), typeof(WriteableBitmap))]
    public class TextureToWritableBitMap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AppSingleton.Instance.TextureCache.getTextureWritableBitMap((Texture)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
