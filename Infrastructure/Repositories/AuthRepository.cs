using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User?> GetByRefreshTokenAsync(
            string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                    x => x.RefreshToken == refreshToken);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}


//using Application.Interfaces;
//using Domain.Entities;
//using Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Repositories
//{
//    public class AuthRepository : IAuthRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public AuthRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<User?> GetByUsernameAsync(string username)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(x => x.Username == username);
//        }

//        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
//        {
//            return await _context.Users
//                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
//        }

//        public async Task<User> CreateAsync(User user)
//        {
//            await _context.Users.AddAsync(user);
//            await _context.SaveChangesAsync();

//            return user;
//        }

//        public async Task UpdateAsync(User user)
//        {
//            _context.Users.Update(user);
//            await _context.SaveChangesAsync();
//        }
//    }
//}


