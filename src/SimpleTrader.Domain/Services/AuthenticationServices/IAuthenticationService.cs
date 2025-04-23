using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

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

        /// <summary>
        /// Login method to authenticate a user with the given username and password.
        /// </summary>
        /// <param name="username">The user's name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task<Account> Login(string username, string password);
    }
}
