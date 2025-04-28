using System.Windows.Input;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISimpleTraderViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(
            INavigator navigator,
            ISimpleTraderViewModelFactory viewModelFactory,
            IAuthenticator authenticator
        )
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _viewModelFactory = viewModelFactory;
            _navigator.StateChanged += OnCurrentViewModelChanged;
            _authenticator.StateChanged += OnIsLoggedInChanged;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(
                navigator,
                _viewModelFactory
            );
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void OnIsLoggedInChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= OnCurrentViewModelChanged;
            _authenticator.StateChanged -= OnIsLoggedInChanged;
            base.Dispose();
        }
    }
}
