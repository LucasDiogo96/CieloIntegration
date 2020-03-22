using Domain.Entities.BaseEntities;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Audience _settings;
        public AuthenticationService(Audience appSettings)
        {
            _settings = appSettings;
        }

        public object Authenticate(string AppName, string AppKey)
        {

            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, _settings.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Secret));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _settings.Iss,
                ValidateAudience = true,
                ValidAudience = _settings.Aud,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: _settings.Iss,
                audience: _settings.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(7)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromDays(7).TotalSeconds
            };

            return responseJson;
        }
    }
}
