using Application.DTOs;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;

            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<string> RegisterAsync(
            RegisterRequestDto request)
        {
            var existingUser =
                await _authRepository
                    .GetByUsernameAsync(request.Username);

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "Username already exists.");
            }

            var user = new User
            {
                Username = request.Username,

                Role = string.IsNullOrWhiteSpace(request.Role)
                    ? "User"
                    : request.Role
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.Password);

            await _authRepository.CreateAsync(user);

            return "User registered successfully.";
        }

        public async Task<LoginResponseDto?> LoginAsync(
                LoginRequestDto request)
        {
            var user = await _authRepository
                .GetByUsernameAsync(request.Username);

            if (user == null)
            {
                return null;
            }

            var result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var accessToken =
                _tokenService.GenerateAccessToken(user);

            var refreshToken =
                _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime =
                DateTime.UtcNow.AddDays(7);

            await _authRepository.UpdateAsync(user);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<string?> RefreshTokenAsync(
            string refreshToken)
        {
            var user =
                await _authRepository
                    .GetByRefreshTokenAsync(refreshToken);

            if (user == null)
            {
                return null;
            }

            if (user.RefreshTokenExpiryTime == null ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            var newAccessToken =
                _tokenService.GenerateAccessToken(user);

            var newRefreshToken =
                _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            user.RefreshTokenExpiryTime =
                DateTime.UtcNow.AddDays(7);

            await _authRepository.UpdateAsync(user);

            return newAccessToken;
        }
    }
}