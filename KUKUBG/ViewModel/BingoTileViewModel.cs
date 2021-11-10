using KUKUBG.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUKUBG.ViewModel
{
    public class BingoTileViewModel : Notify
    {
        /// <summary>
        /// 타일 위치 X
        /// </summary>
        private int x;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;

                OnPropertyChanged("X");
            }
        }
        /// <summary>
        /// 타일 위치 Y
        /// </summary>
        private int y;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;

                OnPropertyChanged("Y");
            }
        }
        /// <summary>
        /// 라벨 표시
        /// </summary>
        public string LabelString => $"{SuggestString}";
        /// <summary>
        /// 상태 0 기본, 1 검정, 2 빨강
        /// </summary>
        private int state;
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;

                OnPropertyChanged("State");
            }
        }
        /// <summary>
        /// 순위
        /// </summary>
        private int order;
        public int Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }
        /// <summary>
        /// 추천 여부
        /// </summary>
        private bool isSuggest = false;
        public bool IsSuggest
        {
            get
            {
                return isSuggest;
            }
            set
            {
                isSuggest = value;

                OnPropertyChanged("IsSuggest");
                OnPropertyChanged("LabelString");
            }
        }
        /// <summary>
        /// 추천 텍스트
        /// </summary>
        private string SuggestString => IsSuggest ? $"추천({Order})" : "";
    }
}
