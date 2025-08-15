using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
namespace ECommerceAPI.Nunit.Tests
{
    public class AuthenticateControllerTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<ILogger<AuthenticateController>> _loggerMock;
        private AuthenticateController _authenticateController;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<AuthenticateController>>();
            _authenticateController = new AuthenticateController(_userRepositoryMock.Object, _tokenServiceMock.Object, _loggerMock.Object);
        }

        [TearDownAttribute]
        public void Cleanup()
        {
            _authenticateController.Dispose();
        }


        [Test]
        public void Login_ValidCredentials_ReturnsUser()
        {
            var login = new Login { Email = "test@example.com", PasswordHash = "password" };
            var user = new User { Id = 1, Email = login.Email };
            var expectedToken = "mock-jwt-token";

            _userRepositoryMock.Setup(repo => repo.Login(login))
                .Returns(user);
            _tokenServiceMock.Setup(service => service.GenerateToken(user))
                .Returns(expectedToken);

            var result = _authenticateController.Login(login) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            // The result should be an anonymous object with a token property
            var resultValue = result.Value;
            Assert.That(resultValue, Is.Not.Null);

            // Extract the token from the anonymous object
            var tokenProperty = resultValue.GetType().GetProperty("token");
            var actualToken = tokenProperty?.GetValue(resultValue);
            Assert.That(actualToken, Is.EqualTo(expectedToken));
        }

        [Test]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var login = new Login { Email = "test@example.com", PasswordHash = "wrongpassword" };

            _userRepositoryMock.Setup(repo => repo.Login(login))
                .Returns((User)null);

            var result = _authenticateController.Login(login) as UnauthorizedObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(401));
            Assert.That(result.Value, Is.EqualTo("Invalid credentials"));
        }
        
    }
}