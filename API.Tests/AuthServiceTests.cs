using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace API.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _authRepositoryMock =
                new Mock<IAuthRepository>();

            _tokenServiceMock =
                new Mock<ITokenService>();

            _authService =
                new AuthService(
                    _authRepositoryMock.Object,
                    _tokenServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldRegisterUser()
        {
            var request = new RegisterRequestDto
            {
                Username = "rahim",
                Password = "Password@123",
                Role = "User"
            };

            _authRepositoryMock
                .Setup(x => x.GetByUsernameAsync("rahim"))
                .ReturnsAsync((User?)null);

            _authRepositoryMock
                  .Setup(x => x.CreateAsync(It.IsAny<User>()))
                  .ReturnsAsync((User user) => user);

            var result =
                await _authService.RegisterAsync(request);

            Assert.Equal(
                "User registered successfully.",
                result);

            _authRepositoryMock.Verify(
                x => x.GetByUsernameAsync("rahim"),
                Times.Once);

            _authRepositoryMock.Verify(
                x => x.CreateAsync(
                    It.Is<User>(
                        u =>
                            u.Username == "rahim" &&
                            u.Role == "User" &&
                            !string.IsNullOrEmpty(u.PasswordHash))),
                Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrow_WhenUsernameExists()
        {
            var request = new RegisterRequestDto
            {
                Username = "rahim",
                Password = "Password@123",
                Role = "User"
            };

            var existingUser = new User
            {
                Id = 1,
                Username = "rahim",
                Role = "User"
            };

            _authRepositoryMock
                .Setup(x => x.GetByUsernameAsync("rahim"))
                .ReturnsAsync(existingUser);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _authService.RegisterAsync(request));

            _authRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnTokens_WhenCredentialsAreValid()
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = 1,
                Username = "rahim",
                Role = "User"
            };

            user.PasswordHash =
                passwordHasher.HashPassword(
                    user,
                    "Password@123");

            var request = new LoginRequestDto
            {
                Username = "rahim",
                Password = "Password@123"
            };

            _authRepositoryMock
                .Setup(x => x.GetByUsernameAsync("rahim"))
                .ReturnsAsync(user);

            _tokenServiceMock
                .Setup(x => x.GenerateAccessToken(user))
                .Returns("access-token");

            _tokenServiceMock
                .Setup(x => x.GenerateRefreshToken())
                .Returns("refresh-token");

            _authRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var result =
                await _authService.LoginAsync(request);

            Assert.NotNull(result);

            Assert.Equal(
                "access-token",
                result!.AccessToken);

            Assert.Equal(
                "refresh-token",
                result.RefreshToken);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(user),
                Times.Once);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Once);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(user),
                Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsWrong()
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = 1,
                Username = "rahim",
                Role = "User"
            };

            user.PasswordHash =
                passwordHasher.HashPassword(
                    user,
                    "CorrectPassword@123");

            var request = new LoginRequestDto
            {
                Username = "rahim",
                Password = "WrongPassword@123"
            };

            _authRepositoryMock
                .Setup(x => x.GetByUsernameAsync("rahim"))
                .ReturnsAsync(user);

            var result =
                await _authService.LoginAsync(request);

            Assert.Null(result);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(It.IsAny<User>()),
                Times.Never);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Never);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var request = new LoginRequestDto
            {
                Username = "unknown",
                Password = "Password@123"
            };

            _authRepositoryMock
                .Setup(x => x.GetByUsernameAsync("unknown"))
                .ReturnsAsync((User?)null);

            var result =
                await _authService.LoginAsync(request);

            Assert.Null(result);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(It.IsAny<User>()),
                Times.Never);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Never);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnNewAccessToken()
        {
            var user = new User
            {
                Id = 1,
                Username = "rahim",
                Role = "User",
                RefreshToken = "old-refresh-token",
                RefreshTokenExpiryTime =
                    DateTime.UtcNow.AddDays(1)
            };

            _authRepositoryMock
                .Setup(x =>
                    x.GetByRefreshTokenAsync(
                        "old-refresh-token"))
                .ReturnsAsync(user);

            _tokenServiceMock
                .Setup(x =>
                    x.GenerateAccessToken(user))
                .Returns("new-access-token");

            _tokenServiceMock
                .Setup(x =>
                    x.GenerateRefreshToken())
                .Returns("new-refresh-token");

            _authRepositoryMock
                .Setup(x =>
                    x.UpdateAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var result =
                await _authService.RefreshTokenAsync(
                    "old-refresh-token");

            Assert.Equal(
                "new-access-token",
                result);

            Assert.Equal(
                "new-refresh-token",
                user.RefreshToken);

            Assert.True(
                user.RefreshTokenExpiryTime >
                DateTime.UtcNow);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(user),
                Times.Once);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Once);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(user),
                Times.Once);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnNull_WhenTokenInvalid()
        {
            _authRepositoryMock
                .Setup(x =>
                    x.GetByRefreshTokenAsync(
                        "invalid-token"))
                .ReturnsAsync((User?)null);

            var result =
                await _authService.RefreshTokenAsync(
                    "invalid-token");

            Assert.Null(result);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(It.IsAny<User>()),
                Times.Never);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Never);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnNull_WhenTokenExpired()
        {
            var user = new User
            {
                Id = 1,
                Username = "rahim",
                Role = "User",
                RefreshToken = "expired-token",
                RefreshTokenExpiryTime =
                    DateTime.UtcNow.AddMinutes(-10)
            };

            _authRepositoryMock
                .Setup(x =>
                    x.GetByRefreshTokenAsync(
                        "expired-token"))
                .ReturnsAsync(user);

            var result =
                await _authService.RefreshTokenAsync(
                    "expired-token");

            Assert.Null(result);

            _tokenServiceMock.Verify(
                x => x.GenerateAccessToken(It.IsAny<User>()),
                Times.Never);

            _tokenServiceMock.Verify(
                x => x.GenerateRefreshToken(),
                Times.Never);

            _authRepositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<User>()),
                Times.Never);
        }
    }
}
