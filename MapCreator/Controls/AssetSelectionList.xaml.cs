using MapCreatorModels.Models.Assets;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapCreator.Controls
{
    public partial class AssetSelectionList : UserControl
    {
        public static readonly DependencyProperty SelectedAssetProperty =
            DependencyProperty.Register("SelectedAsset",
            typeof(Asset), typeof(AssetSelectionList));

        public Asset SelectedAsset
        {
            get { return (Asset)GetValue(SelectedAssetProperty); }
            set { SetValue(SelectedAssetProperty, value); }
        }

        public static readonly DependencyProperty AssetListProperty =
            DependencyProperty.Register("AssetList",
            typeof(ObservableCollection<Asset>), typeof(AssetSelectionList));

        public ObservableCollection<Asset> AssetList
        {
            get { return (ObservableCollection<Asset>)GetValue(AssetListProperty); }
            set { SetValue(AssetListProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand",
                           typeof(ICommand), typeof(AssetSelectionList));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand",
                   typeof(ICommand), typeof(AssetSelectionList));

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }


        public static readonly DependencyProperty CopyCommandProperty =
            DependencyProperty.Register("CopyCommand",
                   typeof(ICommand), typeof(AssetSelectionList));

        public ICommand CopyCommand
        {
            get { return (ICommand)GetValue(CopyCommandProperty); }
            set { SetValue(CopyCommandProperty, value); }
        }

        public AssetSelectionList()
        {
            InitializeComponent();
        }

        private void AddColisionBox()
        {
        }
    }
}
