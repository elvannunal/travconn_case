using Logs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogsServices(builder.Configuration);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("GatewayOnly");

app.MapControllers();

app.Run();