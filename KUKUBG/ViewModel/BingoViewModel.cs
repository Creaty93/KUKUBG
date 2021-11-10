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
        /// <summary>
        /// 라운드
        /// </summary>
        public int Round => data.Round;
        /// <summary>
        /// 라운드 표시
        /// </summary>
        public string RoundString
        {
            get
            {
                return $"Round {Round} {IsBingoRoundString}";
            }
        }
        /// <summary>
        /// 빙고 라운드 여부
        /// </summary>
        public bool IsBingoRound => data.IsBingoRound;
        /// <summary>
        /// 빙고 라운드 텍스트
        /// </summary>
        public string IsBingoRoundString => data.IsBingoRound ? "(빙고)" : "";
        /// <summary>
        /// 다음 라운드 이동
        /// </summary>
        public void NextRound()
        {
            data.NextRound();

            OnPropertyChanged("Round");
            OnPropertyChanged("RoundString");
        }
        /// <summary>
        /// 타일 정보
        /// </summary>
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
            // 라운드 준비 여부 확인
            bool roundReady = data.RoundReady();
            // 빙고 갯수확인
            int bingoCnt = data.BingoCnt();

            List<SuggestResult> results = new List<SuggestResult>();
            // 라운드 준비됬으면
            if (roundReady)
            {
                // 폭탄 터트림
                data.SetBomb(x, y);
                // 빙고 확인
                int checkBingoCnt = data.BingoCnt();
                // 빙고된게 있으면 적용
                if(checkBingoCnt != bingoCnt)
                {
                    data.ChangeBingo();
                }
                // 다음 라운드 넘김
                NextRound();
                // 추천 타일 가져옴
                results = data.GetSuggestResults();
            }
            // 라운드 준비 안됬으면
            else
            {
                // 타일값만 폭탄으로 변경시켜줌
                data.SetTileState(x, y, 1);
                // 라운드 준비여부 확인
                roundReady = data.RoundReady();
                // 준비 됬으면 추천 타일 가져옴
                if (roundReady)
                {
                    results = data.GetSuggestResults();
                }
            }
            // 추천 타일에서 3순위 안에꺼만 가져옴
            results = results.FindAll(result => result.Order < 4);
            // 타일에 상태값 변경
            foreach (BingoTileViewModel tile in Tiles)
            {
                // 타일에 상태 가져와서 적용
                int state = data.GetTileState(tile.X, tile.Y);
                tile.State = state;
                // 추천 타일 있는지 확인
                SuggestResult findResult = results.Find(result => result.Tile.X == tile.X && result.Tile.Y == tile.Y);
                // 추천 타일 있으면 추천하고 순위 넣어줌
                if(findResult != null)
                {
                    tile.Order = findResult.Order;
                    tile.IsSuggest = true;
                }
                // 추천 타일 아니면 추천 빼줌
                else
                {
                    tile.IsSuggest = false;
                }
            }
        }
    }
}
