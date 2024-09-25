using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using api.Controllers;
using api.Models;

namespace api.UnitTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("supersecretkeythatismorethan32charslong!");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("yourissuer");
            _mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("youraudience");

            _authController = new AuthController(_mockConfiguration.Object);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var authModel = new AuthenticationModel
            {
                Username = "test",
                Password = "password"
            };

            // Act
            var result = _authController.Login(authModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);  // Result property for ActionResult<T>
            Assert.NotNull(okResult.Value);  // Verifica que se devuelva un token
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var authModel = new AuthenticationModel
            {
                Username = "wrong",
                Password = "credentials"
            };

            // Act
            var result = _authController.Login(authModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);  // Result property for ActionResult<T>
        }

        [Fact]
        public void Login_NullCredentials_ReturnsBadRequest()
        {
            // Act
            var result = _authController.Login(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result); // Accessing the Result property
            Assert.Equal("Username y Password son requeridos.", badRequestResult.Value);
        }

        [Fact]
        public void Login_EmptyUsername_ReturnsBadRequest()
        {
            // Arrange
            var authModel = new AuthenticationModel
            {
                Username = "",
                Password = "password"
            };

            // Act
            var result = _authController.Login(authModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result); // Accessing the Result property
            Assert.Equal("Username y Password son requeridos.", badRequestResult.Value);
        }

        [Fact]
        public void Login_EmptyPassword_ReturnsBadRequest()
        {
            // Arrange
            var authModel = new AuthenticationModel
            {
                Username = "test",
                Password = ""
            };

            // Act
            var result = _authController.Login(authModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result); // Accessing the Result property
            Assert.Equal("Username y Password son requeridos.", badRequestResult.Value);
        }
    }
}
