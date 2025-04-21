using System.Windows.Input;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRootSimpleTraderViewModelFactory _viewModelFactory;
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(
            INavigator navigator,
            IRootSimpleTraderViewModelFactory viewModelFactory,
            IAuthenticator authenticator
        )
        {
            Navigator = navigator;
            Authenticator = authenticator;
            _viewModelFactory = viewModelFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(
                navigator,
                _viewModelFactory
            );
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
