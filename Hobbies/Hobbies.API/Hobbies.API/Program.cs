using Hobbies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHobbyInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("GatewayOnly");

app.MapControllers(); 

app.Run();