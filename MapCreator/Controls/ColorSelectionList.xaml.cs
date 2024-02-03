using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapCreator.Controls
{
    /// <summary>
    /// Interaction logic for ColorSelectionList.xaml
    /// </summary>
    public partial class ColorSelectionList : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor",
            typeof(GameColor), typeof(ColorSelectionList));

        public GameColor SelectedColor
        {
            get { return (GameColor)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly DependencyProperty ColorListProperty =
     DependencyProperty.Register("ColorList",
     typeof(List<GameColor>), typeof(ColorSelectionList));

        public List<GameColor> ColorList
        {
            get { return (List<GameColor>)GetValue(ColorListProperty); }
            set { SetValue(ColorListProperty, value); }
        }

        public ColorSelectionList()
        {
            InitializeComponent();
            ColorList = new List<GameColor>(GameColorList.GetColors());
        }
    }
}
