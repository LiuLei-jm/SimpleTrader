namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public StockIndexListingViewModel StockIndexListingViewModel { get; set; }

        public HomeViewModel(StockIndexListingViewModel stockIndexListingViewModel)
        {
            StockIndexListingViewModel = stockIndexListingViewModel;
        }
    }
}
