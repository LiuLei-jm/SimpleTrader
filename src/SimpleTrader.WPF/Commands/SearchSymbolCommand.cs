﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly ISearchSymbolViewModel _viewModel;
        private IStockPriceService _stockPriceService;

        public SearchSymbolCommand(
            ISearchSymbolViewModel viewModel,
            IStockPriceService stockPriceService
        )
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISearchSymbolViewModel.CanSearchSymbol))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CanSearchSymbol && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_viewModel.Symbol);
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                _viewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Invalid symbol.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Search symbol failed.";
            }
        }
    }
}
