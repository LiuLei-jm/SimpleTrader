using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(
            SellViewModel sellViewModel,
            ISellStockService sellStockService,
            IAccountStore accountStore
        )
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;

            _sellViewModel.PropertyChanged += SellViewModel_PropertyChanged;
        }

        private void SellViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SellViewModel.CanSellStock))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _sellViewModel.CanSellStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _sellViewModel.ErrorMessage = string.Empty;
            _sellViewModel.StatusMessage = string.Empty;
            try
            {
                string symbol = _sellViewModel.Symbol;
                int shares = _sellViewModel.SharesToSell;
                Account account = await _sellStockService.SellStock(
                    _accountStore.CurrentAccount,
                    _sellViewModel.Symbol,
                    _sellViewModel.SharesToSell
                );
                _accountStore.CurrentAccount = account;

                _sellViewModel.SearchResultSymbol = string.Empty;
                _sellViewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}.";
            }
            catch (InsufficientSharesException ex)
            {
                _sellViewModel.ErrorMessage =
                    $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (InvalidSymbolException)
            {
                _sellViewModel.ErrorMessage = "Invalid symbol.";
            }
            catch (Exception)
            {
                _sellViewModel.ErrorMessage = "Transaction failed";
            }
        }
    }
}
