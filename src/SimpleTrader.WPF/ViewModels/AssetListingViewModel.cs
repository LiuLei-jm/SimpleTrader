using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly Func<
            IEnumerable<AssetViewModel>,
            IEnumerable<AssetViewModel>
        > _filterAssets;
        private readonly ObservableCollection<AssetViewModel> _assets;
        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore)
            : this(assetStore, assets => assets) { }

        public AssetListingViewModel(
            AssetStore assetStore,
            Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets
        )
        {
            _assetStore = assetStore;
            this._filterAssets = filterAssets;
            _assets = new ObservableCollection<AssetViewModel>();
            _assetStore.StateChanged += OnAssetStoreChanged;
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore
                .AssetTransactions.GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(
                    g.Key,
                    g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)
                ))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);
            assetViewModels = _filterAssets(assetViewModels);

            _assets.Clear();
            foreach (var viewModel in assetViewModels)
            {
                _assets.Add(viewModel);
            }
        }

        private void OnAssetStoreChanged()
        {
            ResetAssets();
        }
    }
}
