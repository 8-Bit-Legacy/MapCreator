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
            set
            {
                SetValue(BoundTextureProperty, value);
                InitWritableBitmap(value);
                FillWritableBitMapWithTexture(value);
            }
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

        WriteableBitmap WriteableBitmap { get; set; }

        public TextureDrawing()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(DisplayedImage, BitmapScalingMode.NearestNeighbor);
        }

        private void InitWritableBitmap(Texture texture)
        {
            if (texture == null)
                return;

            WriteableBitmap = new WriteableBitmap(
                texture.Width,
                texture.Height,
                1,
                1,
                PixelFormats.Pbgra32,
                null);
            DisplayedImage.Source = WriteableBitmap;
        }

        private void FillWritableBitMapWithTexture(Texture texture)
        {
            try
            {
                WriteableBitmap.Lock();
                for (int y = 0; y < texture.Height; y++)
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        SetPixel(texture.GetColor(x, y), x, y);
                    }
                }
                WriteableBitmap.AddDirtyRect(new Int32Rect(0, 0, BoundTexture.Width, BoundTexture.Height));
            }
            finally
            {
                WriteableBitmap.Unlock();
            }
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        void DrawPixel(MouseEventArgs e)
        {
            if (BoundTexture == null | !IsDrawable)
                return;

            int mouseX = (int)e.GetPosition(DisplayedImage).X;
            int mouseY = (int)e.GetPosition(DisplayedImage).Y;
            int column = (int)((mouseX - 1) / (DisplayedImage.ActualWidth / BoundTexture.Width));
            int row = (int)((mouseY - 1) / (DisplayedImage.ActualHeight / BoundTexture.Height));

            try
            {
                // Reserve the back buffer for updates.
                WriteableBitmap.Lock();
                SetPixel(SelectedColor, column, row);
                // Specify the area of the bitmap that changed.
                WriteableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                WriteableBitmap.Unlock();
            }
        }
        /// <summary>
        /// Toujours lock et unlock le WritableBitMap avant de faire ceci
        /// </summary>
        /// <param name="gameColor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetPixel(GameColor gameColor, int x, int y)
        {
            unsafe
            {
                // Get a pointer to the back buffer.
                IntPtr pBackBuffer = WriteableBitmap.BackBuffer;

                // Find the address of the pixel to draw.
                pBackBuffer += y * WriteableBitmap.BackBufferStride;
                pBackBuffer += x * 4;

                // Compute the pixel's color.
                int color_data = gameColor.Red << 16; // R
                color_data |= gameColor.Green << 8;   // G
                color_data |= gameColor.Blue << 0;   // B

                // Assign the color data to the pixel.
                *(int*)pBackBuffer = color_data;
            }
            BoundTexture.SetColor(gameColor.Id, x, y);
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
