using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class StockIndexListingViewModelFactory
        : ISimpleTraderViewModelFactory<StockIndexListingViewModel>
    {
        private readonly IStockIndexService _stockIndexService;

        public StockIndexListingViewModelFactory(IStockIndexService stockIndexService)
        {
            _stockIndexService = stockIndexService;
        }

        public StockIndexListingViewModel CreateViewModel()
        {
            return StockIndexListingViewModel.LoadMajorIndexViewModel(_stockIndexService);
        }
    }
}
