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
                //FillWritableBitMapWithTexture(value);
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
            WriteableBitmap = new WriteableBitmap(
                16,
                16,
                1,
                1,
                PixelFormats.Pbgra32,
            null);
            DisplayedImage.Source = WriteableBitmap;
            DisplayedImage.UpdateLayout();
        }

        private void InitWritableBitmap(Texture texture)
        {

            if (texture == null)
                return;


        }

        private void FillWritableBitMapWithTexture(Texture texture)
        {
            byte[] ColorData = { 255, 255, 255, 255 }; // B G R
            try
            {
                //WriteableBitmap.Lock();
                for (int y = 0; y < texture.Height; y++)
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        Int32Rect rect = new Int32Rect(
                    x,
                    y,
                    1,
                    1);
                        WriteableBitmap.WritePixels(rect, ColorData, 4, 0);
                        //unsafe
                        //{
                        //    // Get a pointer to the back buffer.
                        //    IntPtr pBackBuffer = WriteableBitmap.BackBuffer;

                        //    // Find the address of the pixel to draw.
                        //    pBackBuffer += y * WriteableBitmap.BackBufferStride;
                        //    pBackBuffer += x * 4;

                        //    // Compute the pixel's color.
                        //    int color_data = 255 << 16; // R
                        //    color_data |= 128 << 8;   // G
                        //    color_data |= 255 << 0;   // B

                        //    // Assign the color data to the pixel.
                        //    *(int*)pBackBuffer = color_data;
                        //}
                    }
                }
                //WriteableBitmap.AddDirtyRect(new Int32Rect(1, 1, 1, 1));
            }
            finally
            {
                //WriteableBitmap.Unlock();
            }
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        void DrawPixel(MouseEventArgs e)
        {
            if (BoundTexture == null)
                return;

            int column = (int)e.GetPosition(DisplayedImage).X / ((int)this.ActualHeight / BoundTexture.Width);
            int row = (int)e.GetPosition(DisplayedImage).Y / ((int)this.Width / BoundTexture.Height);

            try
            {
                // Reserve the back buffer for updates.
                WriteableBitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = WriteableBitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += row * WriteableBitmap.BackBufferStride;
                    pBackBuffer += column * 4;

                    // Compute the pixel's color.
                    int color_data = 255 << 16; // R
                    color_data |= 128 << 8;   // G
                    color_data |= 255 << 0;   // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }

                // Specify the area of the bitmap that changed.
                WriteableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                WriteableBitmap.Unlock();
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
