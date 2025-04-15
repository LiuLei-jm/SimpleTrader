using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public StockIndexViewModel StockIndexViewModel { get; set; }

        public HomeViewModel(StockIndexViewModel stockIndexViewModel)
        {
            StockIndexViewModel = stockIndexViewModel;
        }
    }
}
