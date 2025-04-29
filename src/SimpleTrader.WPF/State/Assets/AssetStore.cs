using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Accounts;

namespace SimpleTrader.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;
        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssetTransaction> AssetTransactions =>
            _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();
        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.StateChanged += OnAccountStoreChanged;
        }

        private void OnAccountStoreChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
