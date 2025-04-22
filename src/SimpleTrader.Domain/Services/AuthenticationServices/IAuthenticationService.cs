using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        EmailAlreadyExists,
        UserAlreadyExists,
        InvalidEmail,
        InvalidUsername,
        PasswordTooWeak,
        PasswordsDoNotMatch
    }
    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<Account> Login(string username, string password);
    }
}
