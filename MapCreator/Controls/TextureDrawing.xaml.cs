using MapCreator.Cache;
using MapCreatorModels.Models.Assets;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

namespace MapCreator.Controls
{
    /// <summary>
    /// Interaction logic for TextureDrawing.xaml
    /// </summary>
    public partial class TextureDrawing : UserControl
    {
        #region DataBinding
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor",
         typeof(GameColor), typeof(TextureDrawing));

        public GameColor SelectedColor
        {
            get { return (GameColor)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly DependencyProperty BoundTextureProperty =
            DependencyProperty.Register("BoundTexture",
                        typeof(Texture), typeof(TextureDrawing));
        public Texture BoundTexture
        {
            get { return (Texture)GetValue(BoundTextureProperty); }
            set{ SetValue(BoundTextureProperty, value); }
        }

        public static readonly DependencyProperty IsDrawableProperty =
            DependencyProperty.Register("IsDrawable",
                        typeof(bool), typeof(TextureDrawing));

        public bool IsDrawable
        {
            get { return (bool)GetValue(IsDrawableProperty); }
            set { SetValue(IsDrawableProperty, value); }
        }

        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register("ShowAlpha",
                typeof(bool), typeof(TextureDrawing));

        public bool ShowAlpha
        {
            get { return (bool)GetValue(ShowAlphaProperty); }
            set { SetValue(ShowAlphaProperty, value); }
        }
        #endregion

        public TextureDrawing()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(DisplayedImage, BitmapScalingMode.NearestNeighbor);
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        void DrawPixel(MouseEventArgs e)
        {
            if (BoundTexture == null || !IsDrawable || SelectedColor == null)
                return;
            WriteableBitmap bitmap = (WriteableBitmap)DisplayedImage.Source;

            int mouseX = (int)e.GetPosition(DisplayedImage).X;
            int mouseY = (int)e.GetPosition(DisplayedImage).Y;
            int column = (int)((mouseX - 1) / (DisplayedImage.ActualWidth / BoundTexture.Width));
            int row = (int)((mouseY - 1) / (DisplayedImage.ActualHeight / BoundTexture.Height));

            try
            {
                // Reserve the back buffer for updates.
                bitmap.Lock();
                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = bitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += row * bitmap.BackBufferStride;
                    pBackBuffer += column * 4;

                    // Compute the pixel's color.
                    int color_data = SelectedColor.Red << 16; // R
                    color_data |= SelectedColor.Green << 8;   // G
                    color_data |= SelectedColor.Blue << 0;   // B

                    // Assign the color data to the pixel.
                    *(int*)pBackBuffer = color_data;
                }
                BoundTexture.SetColor(SelectedColor.Id, column, row);
                // Specify the area of the bitmap that changed.
                bitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                bitmap.Unlock();
            }
        }

        private void DisplayedImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DrawPixel(e);
        }

        private void DisplayedImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DrawPixel(e);
        }
    }
}
