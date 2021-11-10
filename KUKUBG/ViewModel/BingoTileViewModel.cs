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
        public string LocationString => $"{SuggestString}";
        // 0 기본, 1 검정, 2 빨강
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
                OnPropertyChanged("LocationString");
            }
        }

        private string SuggestString => IsSuggest ? $"추천({Order})" : "";
    }
}
