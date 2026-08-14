using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByRefreshTokenAsync(string refreshToken);

        Task<User> CreateAsync(User user);

        Task UpdateAsync(User user);
    }
}

