﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public AssetListingViewModel AssetListingViewModel { get; }
        private readonly IStockPriceService stockPriceService;
        private AssetViewModel _selectedAsset;
        public AssetViewModel SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
                OnPropertyChanged(nameof(Symbol));
                OnPropertyChanged(nameof(CanSearchSymbol));
            }
        }

        private string _symbol;
        public string Symbol => SelectedAsset?.Symbol ?? string.Empty;
        public bool CanSearchSymbol => !string.IsNullOrEmpty(Symbol);
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
        private int _sharesToSell;
        public int SharesToSell
        {
            get { return _sharesToSell; }
            set
            {
                if (_sharesToSell != value)
                {
                    _sharesToSell = value;
                    OnPropertyChanged(nameof(SharesToSell));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(CanSellStock));
                }
            }
        }

        public bool CanSellStock => SharesToSell > 0;
        public double TotalPrice => SharesToSell * StockPrice;
        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }
        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public SellViewModel(
            AssetStore assetStore,
            IStockPriceService stockPriceService,
            IAccountStore accountStore,
            ISellStockService sellStockService
        )
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
            AssetListingViewModel = new AssetListingViewModel(assetStore);
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);
        }

        public override void Dispose()
        {
            AssetListingViewModel.Dispose();
            ErrorMessageViewModel.Dispose();
            StatusMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
