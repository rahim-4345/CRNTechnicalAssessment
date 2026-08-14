using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var jwtSettings =
                _configuration.GetSection("Jwt");

            var key = jwtSettings["Key"]
                ?? throw new InvalidOperationException(
                    "JWT Key is missing.");

            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var minutes =
                int.TryParse(
                    jwtSettings["AccessTokenMinutes"],
                    out var value)
                    ? value
                    : 15;

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes =
                RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
    }
}

//using Application.Interfaces;
//using Domain.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace Infrastructure.Services
//{
//    public class JwtTokenService : ITokenService
//    {
//        private readonly IConfiguration _configuration;

//        public JwtTokenService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public string GenerateAccessToken(User user)
//        {
//            var jwtSettings = _configuration.GetSection("Jwt");

//            var key = jwtSettings["Key"]
//                ?? throw new InvalidOperationException("JWT Key is missing.");

//            var issuer = jwtSettings["Issuer"];
//            var audience = jwtSettings["Audience"];

//            var minutes = int.Parse(
//                jwtSettings["AccessTokenMinutes"] ?? "15");

//            var securityKey =
//                new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(key));

//            var credentials =
//                new SigningCredentials(
//                    securityKey,
//                    SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//            {
//                new Claim(
//                    ClaimTypes.NameIdentifier,
//                    user.Id.ToString()),

//                new Claim(
//                    ClaimTypes.Name,
//                    user.Username),

//                new Claim(
//                    ClaimTypes.Role,
//                    user.Role)
//            };

//            var token = new JwtSecurityToken(
//                issuer: issuer,
//                audience: audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(minutes),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler()
//                .WriteToken(token);
//        }

//        public string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[64];

//            using var rng =
//                RandomNumberGenerator.Create();

//            rng.GetBytes(randomNumber);

//            return Convert.ToBase64String(randomNumber);
//        }
//    }
//}