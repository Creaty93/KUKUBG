﻿using KUKUBG.ViewModel;
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
            InitializeComponent();

            model = new BingoViewModel();
            model.Init();
            ListBoxBingoTiles.ItemsSource = model.Tiles;
        }
        private void BingoTileView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ListBoxBingoTiles.SelectedItem != null)
            {
                BingoTileViewModel tileModel = ListBoxBingoTiles.SelectedItem as BingoTileViewModel;
                int x = tileModel.X;
                int y = tileModel.Y;

                model.SetBomb(x, y);
                ListBoxBingoTiles.SelectedItem = null;
            }
        }
    }
}
