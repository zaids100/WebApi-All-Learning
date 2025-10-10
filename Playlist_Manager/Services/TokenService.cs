using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public Task<(string Token, DateTime ExpiresAt)> GenerateAccessTokenAsync(User user)
        {
            var secret = _config["Jwt:Secret"] ?? throw new Exception("JWT secret is missing in configuration.");
            var issuer = _config["Jwt:Issuer"] ?? "PlaylistManagerApi";
            var audience = _config["Jwt:Audience"] ?? "PlaylistManagerClient";

            var expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:AccessTokenMinutes"] ?? "60"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "user"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult((jwt, expires));
        }
    }
}
