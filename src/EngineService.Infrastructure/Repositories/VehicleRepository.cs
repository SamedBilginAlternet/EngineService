// src/EngineService.Infrastructure/Repositories/VehicleRepository.cs
using EngineService.EngineService.Domain.Entitities;
using EngineService.EngineService.Domain.Interfaces;
using EngineService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EngineService.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly EngineServiceDbContext _db;
        public VehicleRepository(EngineServiceDbContext db)
            => _db = db;

        public async Task AddAsync(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _db.Vehicles.FindAsync(id);
            if (entity == null) return;
            _db.Vehicles.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
            => await _db.Vehicles.AsNoTracking().ToListAsync();

        public async Task<Vehicle> GetByIdAsync(Guid id)
            => await _db.Vehicles
                         .AsNoTracking()
                         .FirstOrDefaultAsync(v => v.Id == id);

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _db.Vehicles.Update(vehicle);
            await _db.SaveChangesAsync();
        }
    }
}
