using MapCreatorModels.Models.Assets;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MapCreator.Controls
{
    /// <summary>
    /// Interaction logic for TileSelectionList.xaml
    /// </summary>
    public partial class TileSelectionList : UserControl
    {
        public static readonly DependencyProperty SelectedTileProperty =
            DependencyProperty.Register("SelectedTile",
            typeof(Tile), typeof(TileSelectionList));

        public GameColor SelectedTile
        {
            get { return (GameColor)GetValue(SelectedTileProperty); }
            set { SetValue(SelectedTileProperty, value); }
        }

        public static readonly DependencyProperty TileListProperty =
            DependencyProperty.Register("TileList",
            typeof(ObservableCollection<Tile>), typeof(TileSelectionList));

        public ObservableCollection<Tile> TileList
        {
            get { return (ObservableCollection<Tile>)GetValue(TileListProperty); }
            set { SetValue(TileListProperty, value); }
        }

        public TileSelectionList()
        {
            InitializeComponent();
        }
    }
}
