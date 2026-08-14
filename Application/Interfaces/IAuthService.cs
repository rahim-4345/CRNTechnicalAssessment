using Application.DTOs;
using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto request);

        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

        Task<string?> RefreshTokenAsync(string refreshToken);
    }
}



