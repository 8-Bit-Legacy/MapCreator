using CommunityToolkit.Mvvm.Input;
using MapCreator.Windows;
using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MapCreator
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Asset> TileList { get; set; }

        public ObservableCollection<Asset> ActorList { get; set; }

        public ICommand AddTileCommand { get; }

        private void AddTile(object obj)
        {
            AppSingleton.Instance.TileFactory.CreateAsset("New Tile");
        }
        public ICommand DeleteTileCommand { get;}
        private void DeleteTile(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                AppSingleton.Instance.TileFactory.DeleteAsset((byte)obj);
        }
        public ICommand EditTileCommand { get; }
        private void EditTile(object obj)
        {
            Texture texture = AppSingleton.Instance.TileFactory.GetTileById((byte)obj).Texture;
            TextureDrawerWindow textureDrawerWindow = new TextureDrawerWindow(texture);
            textureDrawerWindow.ShowDialog();
        }
        public ICommand CopyTileCommand { get; }
        private void CopyTile(object obj)
        {
            AppSingleton.Instance.TileFactory.CopyAsset((byte)obj);
        }

        private bool CanCmdExec(object obj) => true;
        public Asset SelectedAsset { get; set; }
        public Actor SelectedActor { get; set; }
        Map Map { get; set; }

        public MainWindowViewModel()
        {
            TileList = AppSingleton.Instance.TileFactory.GetObservableCollection();
            ActorList = AppSingleton.Instance.ActorFactory.GetObservableCollection();
            Map = AppSingleton.Instance.Map;

            AddTileCommand = new RelayCommand<object>(AddTile, CanCmdExec);
            DeleteTileCommand = new RelayCommand<object>(DeleteTile, CanCmdExec);
            EditTileCommand = new RelayCommand<object>(EditTile, CanCmdExec);
            CopyTileCommand = new RelayCommand<object>(CopyTile, CanCmdExec);
        }
    }
}
