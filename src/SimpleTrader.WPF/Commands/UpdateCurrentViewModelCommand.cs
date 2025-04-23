using System.Windows.Input;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : AsyncCommandBase
    {
        private INavigator _navigator;
        private readonly ISimpleTraderViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(
            INavigator navigator,
            ISimpleTraderViewModelFactory viewModelFactory
        )
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                Task.CompletedTask.Wait();
            }
            else
            {
                throw new ArgumentException(
                    "Parameter is not of type ViewType.",
                    nameof(parameter)
                );
            }
        }
    }
}
