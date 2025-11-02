using Identity.Application.DTOs;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Services;
public  class AuthService : IAuthService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IApplicationDbContext dbContext,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    // --- Kayıt İşlemi ---
    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        // 1. Kullanıcının zaten var olup olmadığını kontrol et
        var existingAccount = await _dbContext.Accounts
            .AnyAsync(a => a.Email == registerDto.Email);

        if (existingAccount)
        {
            _logger.LogWarning("Registration failed: User with email {Email} already exists.", registerDto.Email);
            return false;
        }

        // 2. Şifreyi hash'le (DbContext seed'inde olduğu gibi null! argümanı kabul edilebilir varsayımıyla)
        // Bu, Account nesnesinin zorunlu alanı olan PasswordHash'i hesaplamamızı sağlar.
        var hashedPassword = _passwordHasher.HashPassword(null!, registerDto.Password);
        
        // 3. Yeni Account nesnesi oluştur ve zorunlu alan olan PasswordHash'i object initializer içinde set et.
        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            Email = registerDto.Email,
            Role = "User", 
            PasswordHash = hashedPassword // << PasswordHash hatası burada çözüldü.
        };

        // 4. Veritabanına ekle
        _dbContext.Accounts.Add(newAccount);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("New user registered successfully: {UserId}", newAccount.Id);
        
        return true;
    }

    // --- Giriş İşlemi ---
    public async Task<TokenResponseDto?> LoginAsync(LoginDto loginDto)
    {
        // 1. Kullanıcıyı e-posta ile bul
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Email == loginDto.Email);

        if (account == null)
        {
            _logger.LogWarning("Login failed: User not found for email: {Email}", loginDto.Email);
            return null;
        }

        // 2. Şifreyi doğrula
        var isPasswordValid = _passwordHasher.VerifyPassword(account, loginDto.Password);
        
        if (!isPasswordValid)
        {
            _logger.LogWarning("Login failed: Invalid password for user: {Email}", loginDto.Email);
            return null;
        }

        // 3. JWT Token oluştur
        var tokenString = await _tokenService.CreateTokenAsync(account);

        // 4. Token yanıtı DTO'sunu döndür
        return new TokenResponseDto
        {
            Token = tokenString,
            // Guid'i string'e çevirerek hatalı dönüşümü düzelttik.
            UserId = account.Id, 
            Email = account.Email,
            Role = account.Role
        };
    }

    public async Task<IEnumerable<UserListDto>> GetAllUsersAsync()
    {
        var users = await _dbContext.Accounts
            .AsNoTracking()
            .ToListAsync();

        return users.Select(a => new UserListDto
        {
            Id = a.Id.ToString(),
            Email = a.Email,
            FirstName = a.FirstName,
            Role = a.Role
        });
    }
}