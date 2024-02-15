using MapCreatorModels.Models.Assets;
using MapCreatorModels.DAL;
using System.Windows;
using MapCreator.Windows;
using System.Windows.Media;

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
            //Force le constructeur
            AppSingleton app = AppSingleton.Instance;
            this.DataContext = new MainWindowViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppSingleton.Instance.SaveState();
        }
    }
}