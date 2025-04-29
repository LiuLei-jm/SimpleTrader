using System.Collections.ObjectModel;
using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _topAssets;
        public double AccountBalance => _assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(assetStore,assets => assets.Take(3));
            _assetStore.StateChanged += OnAssetStoreChanged;
        }

        private void OnAssetStoreChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }
        public override void Dispose()
        {
            _assetStore.StateChanged -= OnAssetStoreChanged;
            AssetListingViewModel.Dispose();
            base.Dispose();
        }
    }
}
