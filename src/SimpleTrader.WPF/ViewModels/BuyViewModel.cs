using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }
        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set
            {
                if (_searchResultSymbol != value)
                {
                    _searchResultSymbol = value;
                    OnPropertyChanged(nameof(SearchResultSymbol));
                }
            }
        }
        private double _stockPrice;
        public double StockPrice
        {
            get { return _stockPrice; }
            set
            {
                if (_stockPrice != value)
                {
                    _stockPrice = value;
                    OnPropertyChanged(nameof(StockPrice));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        private int _sharesToBuy;
        public int SharesToBuy
        {
            get { return _sharesToBuy; }
            set
            {
                if (_sharesToBuy != value)
                {
                    _sharesToBuy = value;
                    OnPropertyChanged(nameof(SharesToBuy));
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public double TotalPrice
        {
            get { return StockPrice * SharesToBuy; }
        }
        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService,IAccountStore accountStore)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }
    }
}
