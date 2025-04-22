using Microsoft.AspNet.Identity;
using Moq;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(
                _mockAccountService.Object,
                _mockPasswordHasher.Object
            );
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsernameAsync()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService
                .Setup(s => s.GetByUsername(expectedUsername))
                .ReturnsAsync(
                    new Models.Account()
                    {
                        AccountHolder = new Models.User() { Username = expectedUsername },
                    }
                );
            _mockPasswordHasher
                .Setup(s => s.VerifyHashedPassword(It.IsAny<String>(), password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            var account = await _authenticationService.Login(expectedUsername, password);

            // Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockAccountService
                .Setup(s => s.GetByUsername(expectedUsername))
                .ReturnsAsync(
                    new Models.Account()
                    {
                        AccountHolder = new Models.User() { Username = expectedUsername },
                    }
                );
            _mockPasswordHasher
                .Setup(s => s.VerifyHashedPassword(It.IsAny<String>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            var exception = Assert.ThrowsAsync<InvalidPasswordException>(
                () => _authenticationService.Login(expectedUsername, password)
            );

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockPasswordHasher
                .Setup(s => s.VerifyHashedPassword(It.IsAny<String>(), password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            var exception = Assert.ThrowsAsync<UserNotFoundException>(
                () => _authenticationService.Login(expectedUsername, password)
            );

            // Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";
            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;

            RegistrationResult actual = await _authenticationService.Register(
                It.IsAny<string>(),
                It.IsAny<string>(),
                password,
                confirmPassword
            );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            string password = "testpassword";
            string confirmPassword = "testpassword";
            string email = "test@gmail.com";
            _mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Models.Account());
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            RegistrationResult actual = await _authenticationService.Register(
                email,
                It.IsAny<string>(),
                password,
                confirmPassword
            );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsEmailAlreadyExists()
        {
            string password = "testpassword";
            string confirmPassword = "testpassword";
            string username = "SingletonSean";
            _mockAccountService
                .Setup(s => s.GetByUsername(username))
                .ReturnsAsync(new Models.Account());
            RegistrationResult expected = RegistrationResult.UserAlreadyExists;

            RegistrationResult actual = await _authenticationService.Register(
                It.IsAny<string>(),
                username,
                password,
                confirmPassword
            );
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            RegistrationResult expected = RegistrationResult.Success;

            RegistrationResult actual = await _authenticationService.Register(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            );
            Assert.AreEqual(expected, actual);
        }
    }
}
