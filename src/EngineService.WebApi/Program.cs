using EngineService.Domain.Interfaces;
using EngineService.EngineService.Domain.Interfaces;
using EngineService.Infrastructure.Data;
using EngineService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Connection string & DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EngineServiceDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2) Repository registration – must be before Build()
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

builder.Services.AddScoped<IServiceRecordRepository, ServiceRecordRepository>();
builder.Services.AddScoped<IServiceFileRepository, ServiceFileRepository>();

// 3) MVC / API + Swagger (only once)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4) Build the app
var app = builder.Build();

// 5) HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
