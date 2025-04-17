using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : ISimpleTraderViewModelFactory<HomeViewModel>
    {
        private readonly ISimpleTraderViewModelFactory<StockIndexListingViewModel> _stockIndexListingFactory;

        public HomeViewModelFactory(
            ISimpleTraderViewModelFactory<StockIndexListingViewModel> stockIndexListingFactory
        )
        {
            _stockIndexListingFactory = stockIndexListingFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_stockIndexListingFactory.CreateViewModel());
        }
    }
}
