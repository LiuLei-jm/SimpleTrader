using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;

namespace SimpleTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }
        event Action StateChanged;
        Task<RegistrationResult> Register(
            string email,
            string username,
            string password,
            string confirmPassword
        );
        Task<bool> Login(string username, string password);
        void Logout();
    }
}
