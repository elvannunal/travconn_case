using Identity.Application;
using Identity.Infrastructure;
using Identity.Application.Settings; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService();

//controllers 
builder.Services.AddControllers();

//  JwtSettings'ı yapılandırması
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

// CORS - Sadece Gateway'den gelen isteklere izin veririz
builder.Services.AddCors(options =>
{
    options.AddPolicy("GatewayOnly", policy =>
    {
        policy.WithOrigins("http://localhost:5000") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

//  JWT Authentication 
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey!)),

            ValidateIssuer = true, 
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true, 
            ValidAudience = jwtSettings.Audience,
            
            ValidateLifetime = true, 
            ClockSkew = TimeSpan.Zero 
        };
    });

//  Authorization servisi
builder.Services.AddAuthorization();


var app = builder.Build();

// --- Middleware Pipeline ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware
app.UseCors("GatewayOnly");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();
app.Run();
