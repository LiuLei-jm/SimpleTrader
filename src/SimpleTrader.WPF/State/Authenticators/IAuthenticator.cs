﻿using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
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
        /// <summary>
        /// Login method to authenticate a user with the given username and password.
        /// </summary>
        /// <param name="username">The user's name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task Login(string username, string password);
        void Logout();
    }
}
