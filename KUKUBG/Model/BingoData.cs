using KUKUBG.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUKUBG.Model
{    
    /// <summary>
     /// 타일에 위치
     /// </summary>
    public struct TileLocation
    {
        public int X;
        public int Y;
    }
    /// <summary>
    /// 추천결과
    /// </summary>
    public class SuggestResult
    {
        /// <summary>
        /// 추천 위치
        /// </summary>
        public TileLocation Tile;
        /// <summary>
        /// 점수
        /// </summary>
        public int Score;
        /// <summary>
        /// 우선순위
        /// </summary>
        public int Order;
    }
    /// <summary>
    /// 빙고 데이터
    /// </summary>
    [Serializable]
    public class BingoData
    {
        /// <summary>
        /// 라운드
        /// </summary>
        public int Round = 1;
        /// <summary>
        /// 빙고라운드 여부
        /// </summary>
        public bool IsBingoRound => Round % 3 == 0;
        /// <summary>
        /// 빙고 초기화
        /// </summary>
        public void InitRound()
        {
            Round = 1;
        }
        /// <summary>
        /// 다음 라운드로 이동
        /// </summary>
        public void NextRound()
        {
            Round++;
        }
        public bool RoundReady()
        {
            List<TileLocation> tiles = GetTiles(1, 2);

            return tiles.Count >= 2;
        }
        /// <summary>
        /// 타일별 흰색 장판일 때 점수
        /// </summary>
        private readonly int[,] tileScores =
        {
            {1,1,1,1,1 },
            {1,3,3,2,1 },
            {1,3,3,2,1 },
            {1,2,2,2,1 },
            {1,1,1,1,1 }
        };
        /// <summary>
        /// 타일 점수 가져오기
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        /// <returns>점수</returns>
        public int GetTileScore(int x, int y)
        {
            return tileScores[x, y];
        }
        /// <summary>
        /// 빙고 데이터의 점수 가져오기
        /// </summary>
        /// <param name="data">빙고 데이터</param>
        /// <returns>점수</returns>
        public int GetBingoScore()
        {
            int score = 0;
            // 흰색 타일을 가져옴
            List<TileLocation> whiteTiles = GetTiles(0);
            // 흰색 타일에 점수를 가져옴
            foreach(TileLocation whiteTile in whiteTiles)
            {
                score += GetTileScore(whiteTile.X, whiteTile.Y);
            }

            return score;
        }
        /// <summary>
        /// 타일에 상태 0 : 흰색, 1 : 검정색, 2 : 빨간색
        /// </summary>
        private int[,] tileStates =
        {
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 }
        };
        /// <summary>
        /// 타일 초기화
        /// </summary>
        public void InitTiles()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    SetTileState(x, y, 0);
                }
            }
        }
        /// <summary>
        /// 타일 상태가져오기
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        /// <returns>타일 상태</returns>
        public int GetTileState(int x, int y)
        {
            return tileStates[x, y];
        }
        /// <summary>
        /// 타일 상태 설정하기
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        /// <param name="state">타일 상태 0 : 흰색, 1 : 검정색, 2 : 빨간색</param>
        public void SetTileState(int x, int y, int state)
        {
           if(state != tileStates[x, y]) tileStates[x, y] = state;
        }
        /// <summary>
        /// 빙고 갯수구하기
        /// </summary>
        /// <returns>빙고수</returns>
        public int BingoCnt()
        {
            // 빙고수
            int cnt = 0;
            // x축 빙고 확인
            for (int x = 0; x < 5; x++)
            {
                if (tileStates[x, 0] != 0 && tileStates[x, 1] != 0 && tileStates[x, 2] != 0 && tileStates[x, 3] != 0 && tileStates[x, 4] != 0)
                {
                    cnt++;
                }
            }
            // y축빙고 확인
            for (int y = 0; y < 5; y++)
            {
                if (tileStates[0, y] != 0 && tileStates[1, y] != 0 && tileStates[2, y] != 0 && tileStates[3, y] != 0 && tileStates[4, y] != 0)
                {
                    cnt++;
                }
            }
            // 대각선 빙고 확인
            if (tileStates[0, 0] != 0 && tileStates[1, 1] != 0 && tileStates[2, 2] != 0 && tileStates[3, 3] != 0 && tileStates[4, 4] != 0)
            {
                cnt++;
            }
            if (tileStates[0, 4] != 0 && tileStates[1, 3] != 0 && tileStates[2, 2] != 0 && tileStates[3, 1] != 0 && tileStates[4, 0] != 0)
            {
                cnt++;
            }

            return cnt;
        }
        /// <summary>
        /// 빙고 변환 시키기
        /// </summary>
        public void ChangeBingo()
        {
            // x축 빙고 확인
            for (int x = 0; x < 5; x++)
            {
                if (tileStates[x, 0] != 0 && tileStates[x, 1] != 0 && tileStates[x, 2] != 0 && tileStates[x, 3] != 0 && tileStates[x, 4] != 0)
                {
                    SetTileState(x, 0, 2);
                    SetTileState(x, 1, 2);
                    SetTileState(x, 2, 2);
                    SetTileState(x, 3, 2);
                    SetTileState(x, 4, 2);
                }
            }
            // y축빙고 확인
            for (int y = 0; y < 5; y++)
            {
                if (tileStates[0, y] != 0 && tileStates[1, y] != 0 && tileStates[2, y] != 0 && tileStates[3, y] != 0 && tileStates[4, y] != 0)
                {
                    SetTileState(0, y, 2);
                    SetTileState(1, y, 2);
                    SetTileState(2, y, 2);
                    SetTileState(3, y, 2);
                    SetTileState(4, y, 2);
                }
            }
            // 대각선 빙고 확인
            if (tileStates[0, 0] != 0 && tileStates[1, 1] != 0 && tileStates[2, 2] != 0 && tileStates[3, 3] != 0 && tileStates[4, 4] != 0)
            {
                SetTileState(0, 0, 2);
                SetTileState(1, 1, 2);
                SetTileState(2, 2, 2);
                SetTileState(3, 3, 2);
                SetTileState(4, 4, 2);
            }
            if (tileStates[0, 4] != 0 && tileStates[1, 3] != 0 && tileStates[2, 2] != 0 && tileStates[3, 1] != 0 && tileStates[4, 0] != 0)
            {
                SetTileState(0, 4, 2);
                SetTileState(1, 3, 2);
                SetTileState(2, 2, 2);
                SetTileState(3, 1, 2);
                SetTileState(4, 0, 2);
            }
        }
        /// <summary>
        /// 해당 상태값의 타일을 가져온다
        /// </summary>
        /// <param name="states">상태값 파라매터</param>
        /// <returns>타일 위치 정보</returns>
        public List<TileLocation> GetTiles(params int[] states)
        {
            List<TileLocation> tiles = new List<TileLocation>();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    int state = GetTileState(x, y);

                    if(states.Contains(state))
                    {
                        TileLocation tileLocation = new TileLocation()
                        {
                            X = x,
                            Y = y
                        };
                        tiles.Add(tileLocation);
                    }
                }
            }

            return tiles;
        }
        /// <summary>
        /// 지정 타일 상하좌우의 타일 위치들을 가져온다
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        /// <param name="value">칸</param>
        /// <returns>타일 위치정보</returns>
        public List<TileLocation> GetTileLeftRightTopBotoomLocation(int x, int y, int value = 1)
        {
            List<TileLocation> tiles = new List<TileLocation>();

            int left = x - value;
            int right = x + value;
            int top = y - value;
            int bottom = y + value;
            // 왼쪽 타일가져오기
            if(left >= 0)
            {
                TileLocation leftTile = new TileLocation()
                {
                    X = left,
                    Y = y
                };
                tiles.Add(leftTile);
            }
            // 오른쪽 타일 가져오기
            if(right <= 4)
            {
                TileLocation rightTile = new TileLocation()
                {
                    X = right,
                    Y = y
                };
                tiles.Add(rightTile);
            }
            // 위쪽 타일 가져오기
            if (top >= 0)
            {
                TileLocation topTile = new TileLocation()
                {
                    X = x,
                    Y = top
                };
                tiles.Add(topTile);
            }
            // 아래쪽 타일 가져오기
            if (bottom <= 4)
            {
                TileLocation bottomTile = new TileLocation()
                {
                    X = x,
                    Y = bottom
                };
                tiles.Add(bottomTile);
            }

            return tiles;
        }
        /// <summary>
        /// 지정 타일 상하좌우의 2칸 타일 위치들을 가져온다
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        /// <returns>타일 위치정보</returns>
        public List<TileLocation> GetTileDoubleLeftRightTopBotoomLocation(int x, int y)
        {
            List<TileLocation> tiles = new List<TileLocation>();

            List<TileLocation> oneTiles = GetTileLeftRightTopBotoomLocation(x, y);
            List<TileLocation> twoTiles = GetTileLeftRightTopBotoomLocation(x, y, 2);

            tiles.AddRange(oneTiles);
            tiles.AddRange(twoTiles);

            return tiles;
        }
        /// <summary>
        /// 폭탄 설정
        /// </summary>
        /// <param name="x">타일 위치X</param>
        /// <param name="y">타일 위치Y</param>
        public void SetBomb(int x, int y)
        {
            List<TileLocation> boomRange = GetTileLeftRightTopBotoomLocation(x, y);
            boomRange.Add(new TileLocation() { X = x, Y = y });

            foreach(TileLocation boom in boomRange)
            {
                int state = GetTileState(boom.X, boom.Y);

                switch (state)
                {
                    case 0:
                        state = 1;
                        break;
                    case 1:
                        state = 0;
                        break;
                }

                SetTileState(boom.X, boom.Y, state);
            }
        }
        /// <summary>
        /// 추천 타일 위치가져오기
        /// </summary>
        /// <returns>타일 위치</returns>
        public List<SuggestResult> GetSuggestResults()
        {
            // 추천 타일결과
            List<SuggestResult> suggestResults = new List<SuggestResult>();
            // 위험타일 리스트
            List<TileLocation> dangerTiles = GetTiles(1, 2);
            // 추천 타일
            List<TileLocation> suggestTiles = new List<TileLocation>();
            // 위험타일에 상하좌우 2타일과 위험타일을 추천타일로 입력
            foreach(TileLocation dangerTile in dangerTiles)
            {
                List<TileLocation> dangerTileArounds = GetTileDoubleLeftRightTopBotoomLocation(dangerTile.X, dangerTile.Y);

                suggestTiles.AddRange(dangerTileArounds);
                suggestTiles.Add(dangerTile);
            }
            // 중복제거
            suggestTiles = suggestTiles.Distinct().ToList();
            // 추천타일로 추천타일결과 생성
            foreach(TileLocation suggestTile in suggestTiles)
            {
                int score = SuggestScore(this, suggestTile);

                if (score <= 0) continue;

                SuggestResult result = new SuggestResult();
                result.Tile = suggestTile;
                result.Score = score;
                result.Order = 0;

                List<TileLocation> rangeTiles = GetTileDoubleLeftRightTopBotoomLocation(suggestTile.X, suggestTile.Y);
                rangeTiles.Add(suggestTile);

                foreach (TileLocation tile in rangeTiles)
                {
                    int state = GetTileState(tile.X, tile.Y);

                    switch (state)
                    {
                        case 0:
                            if (tile.X == suggestTile.X && tile.Y == suggestTile.Y)
                            {
                                result.Score += 5;
                            }
                            break;
                        case 1:
                            if (tile.X == suggestTile.X && tile.Y == suggestTile.Y)
                            {
                                result.Score -= 5;
                            }
                            else
                            {
                                result.Score -= (int)(GetTileScore(tile.X, tile.Y) * 1.2);
                            }
                            break;
                        case 2:
                            if(tile.X == suggestTile.X && tile.Y == suggestTile.Y)
                            {
                                result.Score -= 10;
                            }
                            else
                            {
                                result.Score -= (int)(GetTileScore(tile.X, tile.Y) * 1.2);
                            }
                            break;
                    }
                }
                rangeTiles.Clear();
                suggestResults.Add(result);
            }
            // 삭제
            suggestTiles.Clear();
            dangerTiles.Clear();

            int lastScore = 99999;
            int order = 0;
            // 추천 타일결과에 score로 order 적용
            foreach (SuggestResult result in suggestResults.OrderByDescending(x => x.Score))
            {
                if(result.Score != lastScore)
                {
                    order++;
                    lastScore = result.Score;
                }

                result.Order = order;
            }

            return suggestResults.OrderBy(x => x.Order).ToList();
        }
        /// <summary>
        /// 추천위치 점수 가져오기
        /// </summary>
        /// <param name="data">빙고데이터</param>
        /// <param name="tile">추천위치</param>
        /// <returns>점수</returns>
        private int SuggestScore(BingoData data, TileLocation tile)
        {
            // 영향안주게 복사해서 사용
            BingoData testData = data.Copy();
            // 지금 빙고수
            int bingoCnt = testData.BingoCnt();
            // 폭탄 터트림
            testData.SetBomb(tile.X, tile.Y);
            // 폭탄 터진 후 빙고 수
            int checkBingoCnt = testData.BingoCnt();
            // 빙고 됬음
            bool changeBingo = checkBingoCnt != bingoCnt;
            // 빙고된게 있으면 빙고 적용
            if (changeBingo)
            {
                testData.ChangeBingo();
            }
            // 빙고라운드
            if (testData.IsBingoRound)
            {
                // 빙고 점수가져옴
                int score = testData.GetBingoScore();
                // 빙고 라운드에 빙고가 안되면 0점 처리
                if (changeBingo == false)
                {
                    score = 0;
                }

                return score;
            }
            // 빙고 라운드 아닌데 빙고 되면 0점처리
            else if (changeBingo)
            {
                return 0;
            }
            // 빙고라운드 아닐때
            else
            {
                // 다음라운드로 넘김
                testData.NextRound();
                // 위험 타일 가져옴
                List<TileLocation> dangerTiles = testData.GetTiles(1, 2);

                List<TileLocation> suggestTiles = new List<TileLocation>();
                //위험 타일에 상하좌우 2타일씩 가져오고 추천 타일에 넣음
                foreach (TileLocation dangerTile in dangerTiles)
                {
                    List<TileLocation> dangerTileArounds = testData.GetTileDoubleLeftRightTopBotoomLocation(dangerTile.X, dangerTile.Y);

                    suggestTiles.AddRange(dangerTileArounds);
                    suggestTiles.Add(dangerTile);
                }
                // 중복제거
                suggestTiles = suggestTiles.Distinct().ToList();

                int suggestScore = 0;
                // 추천 타일별 점수를 가져옴
                foreach (TileLocation suggestTile in suggestTiles)
                {
                    int score = SuggestScore(testData, suggestTile);
                    // 가져온 점수가 기존 추천점수보다 높으면 적용
                    if(score > suggestScore)
                    {
                        suggestScore = score;
                    }
                }

                dangerTiles.Clear();
                suggestTiles.Clear();

                return suggestScore;
            }
        }
        /// <summary>
        /// 빙고데이터 복사
        /// </summary>
        /// <returns>빙고데이터</returns>
        public BingoData Copy()
        {
            return DeepCopy.Copy(this);
        }
    }
}
