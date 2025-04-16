using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class StockIndexListingViewModel : ViewModelBase
    {
        private readonly IStockIndexService _stockIndexService;

        public StockIndexListingViewModel(IStockIndexService stockIndexService)
        {
            _stockIndexService = stockIndexService;
        }

        private StockIndex _dji;
        public StockIndex DJI
        {
            get { return _dji; }
            set
            {
                _dji = value;
                OnPropertyChanged(nameof(DJI));
            }
        }
        private StockIndex _ixic;
        public StockIndex IXIC
        {
            get { return _ixic; }
            set
            {
                _ixic = value;
                OnPropertyChanged(nameof(IXIC));
            }
        }
        private StockIndex _gspc;
        public StockIndex GSPC
        {
            get { return _gspc; }
            set
            {
                _gspc = value;
                OnPropertyChanged(nameof(GSPC));
            }
        }

        public static StockIndexListingViewModel LoadMajorIndexViewModel(
            IStockIndexService stockIndexService
        )
        {
            var stockIndexViewModel = new StockIndexListingViewModel(stockIndexService);
            stockIndexViewModel.LoadStockIndexes();
            return stockIndexViewModel;
        }

        private void LoadStockIndexes()
        {
            _stockIndexService
                .GetStockIndex("DJI")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        DJI = task.Result;
                    }
                });
            _stockIndexService
                .GetStockIndex("IXIC")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        IXIC = task.Result;
                    }
                });
            _stockIndexService
                .GetStockIndex("GSPC")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        GSPC = task.Result;
                    }
                });
        }
    }
}
