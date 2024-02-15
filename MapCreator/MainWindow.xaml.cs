using MapCreatorModels.Models.Assets;
using MapCreatorModels.DAL;
using System.Windows;
using MapCreator.Windows;

namespace MapCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = AppSingleton.Instance.TileFactory.CreateTile("Hi");
            TextureDrawerWindow textureDrawerWindow = new(tile.Texture);
            textureDrawerWindow.Show();
        }
    }
}