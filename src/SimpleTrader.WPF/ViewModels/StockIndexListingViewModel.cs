using System.Windows.Input;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;

namespace SimpleTrader.WPF.ViewModels
{
    public class StockIndexListingViewModel : ViewModelBase
    {
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
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadStockIndexesCommand { get; }

        public StockIndexListingViewModel(IStockIndexService stockIndexService)
        {
            LoadStockIndexesCommand = new LoadStockIndexesCommand(this, stockIndexService);
        }

        public static StockIndexListingViewModel LoadMajorIndexViewModel(
            IStockIndexService stockIndexService
        )
        {
            var stockIndexViewModel = new StockIndexListingViewModel(stockIndexService);
            stockIndexViewModel.LoadStockIndexesCommand.Execute(null);
            return stockIndexViewModel;
        }
    }
}
