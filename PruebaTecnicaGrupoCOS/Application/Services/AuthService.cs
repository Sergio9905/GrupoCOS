using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Helper;
using PruebaTecnicaGrupoCOS.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PruebaTecnicaGrupoCOS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<(string jwtToken, string refreshToken)> AuthenticateUser(string email, string password, string userIp)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !AuthHelper.VerifyPassword(password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var (jwtToken, refreshToken) = GenerateJwtTokens(user, userIp);
            await SaveRefreshToken(user.Id, refreshToken);

            return (jwtToken, refreshToken);
        }

        private async Task SaveRefreshToken(int userId, string refreshToken)
        {
            var user = await _context.UserAccounts.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddMonths(3);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> RefreshJwtToken(string refreshToken, string userIp)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var (newJwtToken, newRefreshToken) = GenerateJwtTokens(user, userIp);
            await SaveRefreshToken(user.Id, newRefreshToken);

            return newJwtToken;
        }

        private (string jwtToken, string refreshToken) GenerateJwtTokens(UserAccount userAccount, string userIp)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt-Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMonths(3);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userAccount.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userAccount.Email),
                new Claim(JwtRegisteredClaimNames.Name, userAccount.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, userAccount.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt-Issuer"],
                audience: _configuration["Jwt-Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            var refreshToken = GenerateRefreshToken();

            return (new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
        }

        private string GenerateRefreshToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
