using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Settings;
using Identity.Application.Interfaces;
using Identity.Application.DTOs;
using Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Identity.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IIdentityLogService _logService;
    private readonly ILogger<TokenService> _logger;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        IIdentityLogService logService,
        ILogger<TokenService> logger)
    {
        _jwtSettings = jwtSettings.Value;
        _logService = logService;
        _logger = logger;
    }

    public async Task<string> CreateTokenAsync(Account account)
    {
        try
        {
            // Claim listesi oluştur
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.Role)
            };

            // Secret key ile imzalama anahtarı
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Token oluşturma
            var tokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Token üretimi logla
            await LogTokenCreationAsync(account, tokenExpiration);

            _logger.LogInformation("Token created successfully for user: {UserId}", account.Id);

            return tokenString;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating token for user: {UserId}", account.Id);
            throw;
        }
    }

    private async Task LogTokenCreationAsync(Account account, DateTime expiration)
    {
        var logDto = new LogTokenRequestDto
        {
            UserId = account.Id,
            LogType = "Token",
            CreatedAt = DateTime.UtcNow,
            Email = account.Email,
            Role = account.Role,
            TokenExpiration = expiration,
            Message = $"JWT token generated for user: {account.Email}"
        };

        // Fire-and-forget pattern - loglama başarısız olsa bile token üretimi etkilenmesin
        _ = Task.Run(async () =>
        {
            try
            {
                await _logService.LogTokenRequestAsync(logDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to log token creation for user: {UserId}", account.Id);
            }
        });
    }
}