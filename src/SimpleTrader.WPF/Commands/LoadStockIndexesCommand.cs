using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class LoadStockIndexesCommand : AsyncCommandBase
    {
        private readonly StockIndexListingViewModel _stockIndexListingViewModel;
        private readonly IStockIndexService _stockIndexService;

        public LoadStockIndexesCommand(
            StockIndexListingViewModel stockIndexListingViewModel,
            IStockIndexService stockIndexService
        )
        {
            _stockIndexListingViewModel = stockIndexListingViewModel;
            _stockIndexService = stockIndexService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _stockIndexListingViewModel.IsLoading = true;

            await Task.WhenAll(LoadDji(), LoadIxic(), LoadGspc());

            _stockIndexListingViewModel.IsLoading = false;
        }

        private async Task LoadDji()
        {
            _stockIndexListingViewModel.DJI = await _stockIndexService.GetStockIndex("DJI");
        }

        private async Task LoadIxic()
        {
            _stockIndexListingViewModel.IXIC = await _stockIndexService.GetStockIndex("IXIC");
        }

        private async Task LoadGspc()
        {
            _stockIndexListingViewModel.GSPC = await _stockIndexService.GetStockIndex("GSPC");
        }
    }
}
