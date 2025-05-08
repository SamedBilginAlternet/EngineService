using EngineService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) ConnectionString'i appsettings.json'dan oku
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2) DbContext'i DI container'a ekle
builder.Services.AddDbContext<EngineServiceDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3) Di�er servis kay�tlar� (ileride Application veya Repository katmanlar�n� da ekleyebilirsiniz)
// builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
// builder.Services.AddScoped<IVehicleService, VehicleService>();

// 4) MVC / API
builder.Services.AddControllers();

// 5) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6) HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
