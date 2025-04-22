using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(
            LoginViewModel loginViewModel,
            IAuthenticator authenticator,
           IRenavigator renavigator
        )
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
            _loginViewModel = loginViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            bool result = await _authenticator.Login(
                _loginViewModel.Username,
                parameter.ToString()
            );
            if (result)
            {
                _renavigator.Renavigate();
            }
        }
    }
}
