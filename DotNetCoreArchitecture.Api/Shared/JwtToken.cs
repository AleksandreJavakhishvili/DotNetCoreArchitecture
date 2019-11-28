using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Api.Shared
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        public JwtTokenGenerator()
        {
        }

        public JwtTokenSettings With(string issuer, string audience, string key, DateTime? expireDate = null)
        {
            return new JwtTokenSettings(Encode(issuer, audience, key, expireDate), Decode(issuer, audience, key), DecodeExpired(issuer, audience, key));
        }

        private static SymmetricSecurityKey SecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        private static Func<Claim[], string> Encode(string issuer, string audience, string key, DateTime? expireDate = null)
        {
            return claims =>
            {
                var credentials = new SigningCredentials(SecurityKey(key), SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(issuer, audience, claims, null, expireDate ?? DateTime.Now.AddMonths(2),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler()
                    .WriteToken(securityToken);
            };
        }

        private Func<string, ClaimsPrincipal> Decode(string issuer, string audience, string key)
        {
            return token =>
            {
                var claims = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = SecurityKey(key),
                    ValidateIssuerSigningKey = true
                }, out var securityToken);


                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return claims;
            };
        }

        private Func<string, ClaimsPrincipal> DecodeExpired(string issuer, string audience, string key)
        {
            return token =>
            {
                var claims = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = SecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false
                }, out var securityToken);


                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return claims;
            };
        }

        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }

    public class JwtTokenSettings
    {
        private readonly Func<Claim[], string> _encodeFunc;
        private readonly Func<string, ClaimsPrincipal> _decodeFunc;
        private readonly Func<string, ClaimsPrincipal> _decodeExpiredFunc;

        public JwtTokenSettings(Func<Claim[], string> encodeFunc,
            Func<string, ClaimsPrincipal> decodeFunc,
            Func<string, ClaimsPrincipal> decodeExpiredFunc)
        {
            _encodeFunc = encodeFunc;
            _decodeFunc = decodeFunc;
            _decodeExpiredFunc = decodeExpiredFunc;
        }

        public string Encode(params Claim[] claims)
        {
            return _encodeFunc(claims);
        }

        public ClaimsPrincipal Decode(string token)
        {
            return _decodeFunc(token);
        }

        public ClaimsPrincipal DecodeExpired(string token)
        {
            return _decodeExpiredFunc(token);
        }

    }
    public interface IJwtTokenGenerator
    {
        JwtTokenSettings With(string issuer, string audience, string key, DateTime? expireDate = null);

        string GetRefreshToken();
    }

}
