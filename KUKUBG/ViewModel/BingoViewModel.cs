using KUKUBG.Commands;
using KUKUBG.Common;
using KUKUBG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KUKUBG.ViewModel
{
    public class BingoViewModel : Notify
    {
        // 빙고 데이터
        private BingoData data;
        public BingoViewModel()
        {
            data = new BingoData();
            Tiles = new List<BingoTileViewModel>();
        }
        public int Round => data.Round;
        public string RoundString
        {
            get
            {
                return $"Round {Round}";
            }
        }

        public void NextRound()
        {
            data.NextRound();

            OnPropertyChanged("Round");
            OnPropertyChanged("RoundString");
        }
        public List<BingoTileViewModel> Tiles { get; set; }
        /// <summary>
        /// 초기화
        /// </summary>
        public void Init()
        {
            data.InitRound();
            data.InitTiles();
            Tiles.Clear();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    BingoTileViewModel tileModel = new BingoTileViewModel();
                    tileModel.X = x;
                    tileModel.Y = y;
                    tileModel.State = 0;
                    tileModel.IsSuggest = false;

                    Tiles.Add(tileModel);
                }
            }

            OnPropertyChanged("RoundString");
        }
        /// <summary>
        /// 폭탄 설정
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        public void SetBomb(int x, int y)
        {
            bool roundReady = data.RoundReady();

            int bingoCnt = data.BingoCnt();

            List<SuggestResult> results = new List<SuggestResult>();

            if (roundReady)
            {
                data.SetBomb(x, y);
                int checkBingoCnt = data.BingoCnt();

                if(checkBingoCnt != bingoCnt)
                {
                    data.ChangeBingo();
                }

                NextRound();

                results = data.GetSuggestResults();
            }
            else
            {
                data.SetTileState(x, y, 1);
                roundReady = data.RoundReady();

                if (roundReady)
                {
                    results = data.GetSuggestResults();
                }
            }
            results = results.FindAll(result => result.Order < 3);

            foreach (BingoTileViewModel tile in Tiles)
            {
                int state = data.GetTileState(tile.X, tile.Y);

                tile.State = state;

                SuggestResult findResult = results.Find(result => result.Tile.X == tile.X && result.Tile.Y == tile.Y);

                if(findResult != null)
                {
                    tile.Order = findResult.Order;
                    tile.IsSuggest = true;
                }
                else
                {
                    tile.IsSuggest = false;
                }
            }
        }
    }
}
