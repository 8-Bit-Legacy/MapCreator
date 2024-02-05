using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapCreator.Controls
{
    /// <summary>
    /// Interaction logic for MapCreator.xaml
    /// </summary>
    public partial class MapDrawer : UserControl
    {
        public static readonly DependencyProperty SelectedAssetProperty =
            DependencyProperty.Register("SelectedAsset",
            typeof(Asset), typeof(MapDrawer));

        public Asset SelectedAsset
        {
            get { return (Asset)GetValue(SelectedAssetProperty); }
            set { SetValue(SelectedAssetProperty, value); }
        }
        public WriteableBitmap Bitmap { get; set; }
        Map map = new Map();

        public MapDrawer()
        {
            InitializeComponent();
            Bitmap = new WriteableBitmap(
                    Map.Width * Texture.Width,
                    Map.Height * Texture.Height,
                    1,
                    1,
                    PixelFormats.Pbgra32,
                    null);
            DisplayedImage.Source = Bitmap;
            RenderOptions.SetBitmapScalingMode(DisplayedImage, BitmapScalingMode.NearestNeighbor);
            map.TextureUpdated += UpdateTexture;
        }

        private void UpdateTexture(object sender, EventArgs args) {
            TextureUpdatedEventArgs eventArg = (TextureUpdatedEventArgs)args;
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    if (map.MapTiles[y,x] != null && map.MapTiles[y,x].Tile.Texture == eventArg.Texture)
                    {
                        DrawTile(y * Texture.Height, x * Texture.Width);
                    }
                }
            }
        }

        private bool UpdateMapTile(int x, int y)
        {
            //Verifier si ta tile a bel et bien besoin d'update
            if (map.MapTiles[y, x] == null || map.MapTiles[y, x].Tile.Texture != SelectedAsset.Texture)
            {
                map.SetTile(x, y, new MapTile((Tile)SelectedAsset));
                return true;
            }

            return false;
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        void DrawTile(int row, int column)
        {
            if (SelectedAsset == null)
                return;

            try
            {
                // Reserve the back buffer for updates.
                Bitmap.Lock();
                unsafe
                {
                    for (int y = 0; y < Texture.Height; y++)
                    {
                        for (int x = 0; x < Texture.Width; x++)
                        {
                            GameColor color = SelectedAsset.Texture.GetColor(x, y);

                            // Get a pointer to the back buffer.
                            IntPtr pBackBuffer = Bitmap.BackBuffer;

                            // Find the address of the pixel to draw.
                            pBackBuffer += (row + y) * Bitmap.BackBufferStride;
                            pBackBuffer += (column + x) * 4;

                            // Compute the pixel's color.
                            int color_data = color.Red << 16; // R
                            color_data |= color.Green << 8;   // G
                            color_data |= color.Blue << 0;   // B

                            // Assign the color data to the pixel.
                            *(int*)pBackBuffer = color_data;
                        }
                    }
                }
                // Specify the area of the bitmap that changed.
                Bitmap.AddDirtyRect(new Int32Rect(column, row, Texture.Width, Texture.Height));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                Bitmap.Unlock();
            }
        }

        private void DrawTileFromMouse(MouseEventArgs e)
        {
            int mouseX = (int)e.GetPosition(DisplayedImage).X;
            int mouseY = (int)e.GetPosition(DisplayedImage).Y;
            //Donne valeurs topLeft de la case
            int column = (int)((mouseX - 1) / (DisplayedImage.ActualWidth / Map.Width));
            int row = (int)((mouseY - 1) / (DisplayedImage.ActualHeight / Map.Height));

            if (UpdateMapTile(column, row))
                DrawTile(row * Texture.Height, column * Texture.Width);
        }

        private void WritableMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DrawTileFromMouse(e);
        }

        private void WritableMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DrawTileFromMouse(e);
        }
    }
}
