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
using System.Windows.Forms;
using MapCreator.Export;

namespace MapCreator
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Asset> TileList { get; set; }

        public ICommand ExportCommand { get; }
        private void Export(object obj)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string selectedPath = folderBrowserDialog.SelectedPath;

            ExportData.ExportMap(AppSingleton.Instance.Map, selectedPath);
            ExportData.ExportTiles(AppSingleton.Instance.TileFactory, selectedPath);
            ExportData.ExportActors(AppSingleton.Instance.ActorFactory, selectedPath);
            ExportData.ExportColors(selectedPath);
            ExportData.ExportColisionMap(AppSingleton.Instance.Map, selectedPath);
        }


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
        public Asset SelectedTile { get; set; }

        public ObservableCollection<Asset> ActorList { get; set; }

        public ICommand AddActorCommand { get; }
        private void AddActor(object obj)
        {
            AppSingleton.Instance.ActorFactory.CreateAsset("Actor");
        }
        public ICommand DeleteActorCommand { get; }
        private void DeleteActor(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                AppSingleton.Instance.ActorFactory.DeleteAsset((byte)obj);
        }
        public ICommand EditActorCommand { get; }
        private void EditActor(object obj)
        {
            Texture texture = AppSingleton.Instance.ActorFactory.GetActorById((byte)obj).Texture;
            TextureDrawerWindow textureDrawerWindow = new TextureDrawerWindow(texture);
            textureDrawerWindow.ShowDialog();
        }
        public ICommand CopyActorCommand { get; }
        private void CopyActor(object obj)
        {
            AppSingleton.Instance.ActorFactory.CopyAsset((byte)obj);
        }
        public Asset SelectedActor { get; set; }

        Map Map { get; set; }
        private bool CanCmdExec(object obj) => true;
        
        public MainWindowViewModel()
        {
            TileList = AppSingleton.Instance.TileFactory.GetObservableCollection();
            ActorList = AppSingleton.Instance.ActorFactory.GetObservableCollection();
            Map = AppSingleton.Instance.Map;

            AddTileCommand = new RelayCommand<object>(AddTile, CanCmdExec);
            DeleteTileCommand = new RelayCommand<object>(DeleteTile, CanCmdExec);
            EditTileCommand = new RelayCommand<object>(EditTile, CanCmdExec);
            CopyTileCommand = new RelayCommand<object>(CopyTile, CanCmdExec);


            AddActorCommand = new RelayCommand<object>(AddActor, CanCmdExec);
            DeleteActorCommand = new RelayCommand<object>(DeleteActor, CanCmdExec);
            EditActorCommand = new RelayCommand<object>(EditActor, CanCmdExec);
            CopyActorCommand = new RelayCommand<object>(CopyActor, CanCmdExec);
            ExportCommand = new RelayCommand<object>(Export, CanCmdExec);
        }
    }
}
