using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelFactory : ISimpleTraderViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;

        public SimpleTraderViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel,
            CreateViewModel<BuyViewModel> createBuyViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel
        )
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createBuyViewModel = createBuyViewModel;
            _createLoginViewModel = createLoginViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Portfolio:
                    return _createPortfolioViewModel();
                case ViewType.Buy:
                    return _createBuyViewModel();
                default:
                    throw new ArgumentException(
                        "The ViewType does not have a VieModel.",
                        "viewType"
                    );
            }
        }
    }
}
