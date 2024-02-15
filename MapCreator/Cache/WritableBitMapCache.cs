using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

namespace MapCreator.Cache
{
    public class WritableBitMapCache
    {
        Dictionary<Texture, WriteableBitmap> WriteableBitmaps { get; set; } = new Dictionary<Texture, WriteableBitmap>();

        public WriteableBitmap getTextureWritableBitMap(Texture texture) {
            if (texture == null)
                return null;

            if (!WriteableBitmaps.TryGetValue(texture, out WriteableBitmap writeableBitmap))
            {
                writeableBitmap = new WriteableBitmap(
                    texture.Width,
                    texture.Height,
                    1,
                    1,
                    PixelFormats.Pbgra32,
                    null);
                FillWritableBitMapWithTexture(texture, writeableBitmap);
                WriteableBitmaps.Add(texture, writeableBitmap);
            }

            return writeableBitmap;
        }

        private void FillWritableBitMapWithTexture(Texture texture, WriteableBitmap writeableBitmap)
        {
            try
            {
                writeableBitmap.Lock();
                for (int y = 0; y < texture.Height; y++)
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        unsafe
                        {
                            // Get a pointer to the back buffer.
                            IntPtr pBackBuffer = writeableBitmap.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += y * writeableBitmap.BackBufferStride;
                            pBackBuffer += x * 4;

                            // Compute the pixel's color.
                            GameColor color = texture.GetColor(x, y);
                            int color_data = 255 << 24; // A
                            color_data = color.Red << 16; // R
                            color_data |= color.Green << 8;   // G
                            color_data |= color.Blue << 0;   // B

                            // Assign the color data to the pixel.
                            *(int*)pBackBuffer = color_data;
                        }
                    }
                }
                writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, texture.Width, texture.Height));
            }
            finally
            {
                writeableBitmap.Unlock();
            }
        }
    }
}
