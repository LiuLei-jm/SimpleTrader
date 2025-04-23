using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(
            RegisterViewModel registerViewModel,
            IAuthenticator authenticator,
            IRenavigator registerRenavigator
        )
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = registerRenavigator;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;
            try
            {
                RegistrationResult registrationResult = await _authenticator.Register(
                    _registerViewModel.Email,
                    _registerViewModel.Username,
                    _registerViewModel.Password,
                    _registerViewModel.ConfirmPassword
                );

                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "Email already exists.";
                        break;
                    case RegistrationResult.UserAlreadyExists:
                        _registerViewModel.ErrorMessage = "User already exists.";
                        break;
                    case RegistrationResult.InvalidEmail:
                        _registerViewModel.ErrorMessage = "Invalid email.";
                        break;
                    case RegistrationResult.InvalidUsername:
                        _registerViewModel.ErrorMessage = "Invalid username.";
                        break;
                    case RegistrationResult.PasswordTooWeak:
                        _registerViewModel.ErrorMessage = "Password too weak.";
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Passwords do not match.";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Unknown error.";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }
    }
}
