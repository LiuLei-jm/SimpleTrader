﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(
            BuyViewModel buyViewModel,
            IBuyStockService buyStockService,
            IAccountStore accountStore
        )
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;

            _buyViewModel.PropertyChanged += BuyViewModel_PropertyChanged;
        }

        private void BuyViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuyViewModel.CanBuyStock))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _buyViewModel.CanBuyStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _buyViewModel.ErrorMessage = string.Empty;
            _buyViewModel.StatusMessage = string.Empty;
            try
            {
                string symbol = _buyViewModel.Symbol;
                int shares = _buyViewModel.SharesToBuy;
                Account account = await _buyStockService.BuyStock(
                    _accountStore.CurrentAccount,
                    _buyViewModel.Symbol,
                    _buyViewModel.SharesToBuy
                );
                _accountStore.CurrentAccount = account;
                _buyViewModel.StatusMessage =
                    $"Successfully purchased {shares} shares of {symbol}.";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage =
                    "Account has insufficient funds. Please transfer more money into your account.";
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Invalid symbol.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Transaction failed";
            }
        }
    }
}
