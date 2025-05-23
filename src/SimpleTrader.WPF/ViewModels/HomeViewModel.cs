﻿namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public StockIndexListingViewModel StockIndexListingViewModel { get; set; }

        public HomeViewModel(
            StockIndexListingViewModel stockIndexListingViewModel,
            AssetSummaryViewModel assetSummaryViewModel
        )
        {
            StockIndexListingViewModel = stockIndexListingViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

        public override void Dispose()
        {
            AssetSummaryViewModel.Dispose();
            StockIndexListingViewModel.Dispose();
            base.Dispose();
        }
    }
}
