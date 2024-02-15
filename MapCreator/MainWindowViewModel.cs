using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreator
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Tile> TileList { get; set; }
        public Tile SelectedTile { get; set; }
        Map map { get; set; }

        public MainWindowViewModel()
        {
                TileList = AppSingleton.Instance.TileFactory.GetTiles();
        }
    }
}
