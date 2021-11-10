using KUKUBG.ViewModel;
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

namespace KUKUBG.View
{
    /// <summary>
    /// BingoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BingoView : UserControl
    {

        BingoViewModel model;
        public BingoView()
        {
            model = new BingoViewModel();
            model.Init();

            InitializeComponent();

            ListBoxBingoTiles.ItemsSource = model.Tiles;
            LabelRound.Content = model.RoundString;
        }
        private void BingoTileView_Selected(object sender, RoutedEventArgs e)
        {
            if (e.Source != null)
            {
                ListBoxItem item = e.Source as ListBoxItem;

                BingoTileViewModel tileModel = (BingoTileViewModel)item.DataContext;
                int x = tileModel.X;
                int y = tileModel.Y;

                model.SetBomb(x, y);

                LabelRound.Content = model.RoundString;
            }
        }

        private void BingoTileView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxBingoTiles.UnselectAll();
        }

        private void BingoTileInit(object sender, RoutedEventArgs e)
        {
            model.Init();
            ListBoxBingoTiles.ItemsSource = null;
            ListBoxBingoTiles.ItemsSource = model.Tiles;
        }
    }
}
