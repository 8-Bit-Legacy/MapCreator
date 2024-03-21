using MapCreatorModels.Models.Assets;
using System.Windows;

namespace MapCreator.Windows
{
    /// <summary>
    /// Interaction logic for TextureDrawer.xaml
    /// </summary>
    public partial class TextureDrawerWindow : Window
    {
        bool toggle;
        TextureDrawerViewModel textureDrawerViewModel;
        Asset window_asset;
        public TextureDrawerWindow(Asset asset)
        {
            InitializeComponent();
            if (asset is Tile)
            {
                IsCollision.IsChecked = ((Tile)asset).isCollision;
            }
            else
            {
                IsVisibleStackPanel.Visibility = Visibility.Collapsed;
            }
            window_asset = asset;
            textureDrawerViewModel = new TextureDrawerViewModel(asset.Texture);
            this.DataContext = textureDrawerViewModel;
        }

        private void ThisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textureDrawerViewModel.DeleteCacheCommand.Execute(null);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (window_asset is Tile)
            {
                ((Tile)window_asset).isCollision = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (window_asset is Tile)
            {
                ((Tile)window_asset).isCollision = false;
            }
        }
    }
}
