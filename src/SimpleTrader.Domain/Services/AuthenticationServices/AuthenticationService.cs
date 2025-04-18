using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountDataService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(
            IAccountService accountDataService,
            IPasswordHasher passwordHasher
        )
        {
            _accountDataService = accountDataService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountDataService.GetByUsername(username);
            if (storedAccount is null)
            {
                throw new UserNotFoundException(username);
            }
            var passwordResult = _passwordHasher.VerifyHashedPassword(
                storedAccount.AccountHolder.PasswordHash,
                password
            );
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }
            return storedAccount;
        }

        public async Task<RegistrationResult> Register(
            string email,
            string username,
            string password,
            string confirmPassword
        )
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            var emailUser = await _accountDataService.GetByEmail(email);

            if (emailUser != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }
            var userAccount = await _accountDataService.GetByUsername(username);

            if (userAccount != null)
            {
                result = RegistrationResult.UserAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DatedJoined = DateTime.Now,
                };

                Account account = new Account() { AccountHolder = user };
                await _accountDataService.Create(account);
            }

            return result;
        }
    }
}
