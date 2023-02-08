﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dom_Phone_server.Services.TokenService.Interfaces;
using Dom_Phone_server.Models;
using Dom_Phone_server.Models.DB;
using Dom_Phone_server.Models.Data;

namespace Dom_Phone_server.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //TODO: add more claims variety and set parameters for claims
        public string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Expired,DateTime.Now.AddMinutes(30).ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Security:AccessKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public RefreshToken GenerateRefreshToken(User user)
        {
            DateTime expiredAt = DateTime.UtcNow.AddDays(30);
            DateTime createdAt = DateTime.UtcNow;
            Guid tokenId = Guid.NewGuid();

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", tokenId.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("ExpiredAt", expiredAt.ToString()),
                new Claim("CreatedAt", createdAt.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Security:RefreshKey").Value!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                Id = tokenId,
                UserId = user.Id,
                User = user,
                Token = jwt,
                CreatedAt = createdAt,
                ExpiredAt = expiredAt,
            };

            return refreshToken;
        }
        public JwtSecurityToken GetJwt(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }

        public bool VerifyRefreshToken(User user, string refreshToken)
        {
            return true;
            throw new NotImplementedException();
        }
    }
}
