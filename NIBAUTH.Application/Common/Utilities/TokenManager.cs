

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NIBAUTH.Application.Common.Utilities
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration) => _configuration = configuration;

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!)),
                ValidateLifetime = false
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT Secret Key is not configured");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            _ = int.TryParse(_configuration["JwtSettings:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            if (tokenValidityInMinutes <= 0) tokenValidityInMinutes = 60; 

            string? securityAlgorithm = _configuration["JwtSettings:SecurityAlgorithms"];
            if (string.IsNullOrWhiteSpace(securityAlgorithm))
                securityAlgorithm = SecurityAlgorithms.HmacSha256;

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, securityAlgorithm)
            );

            return token;
        }

        public string? Decryption(string strText, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(strText) || string.IsNullOrWhiteSpace(privateKey))
                return null;

            try
            {
                var testData = Encoding.UTF8.GetBytes(strText);

                using var rsa = new RSACryptoServiceProvider(1024);
                try
                {
                    rsa.ImportFromPem(privateKey);
                    var resultBytes = Convert.FromBase64String(strText);
                    var decryptedBytes = rsa.Decrypt(resultBytes, false);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}