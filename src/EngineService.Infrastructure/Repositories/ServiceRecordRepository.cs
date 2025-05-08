// src/EngineService.Infrastructure/Repositories/ServiceRecordRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EngineService.Domain.Entities;
using EngineService.Domain.Interfaces;
using EngineService.EngineService.Domain.Entitities;
using EngineService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EngineService.Infrastructure.Repositories
{
    public class ServiceRecordRepository : IServiceRecordRepository
    {
        private readonly EngineServiceDbContext _db;
        public ServiceRecordRepository(EngineServiceDbContext db) => _db = db;

        public async Task AddAsync(ServiceRecord record)
        {
            record.Id = Guid.NewGuid();
            await _db.ServiceRecords.AddAsync(record);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _db.ServiceRecords.FindAsync(id);
            if (entity == null) return;
            _db.ServiceRecords.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceRecord>> GetAllByVehicleAsync(Guid vehicleId)
            => await _db.ServiceRecords
                        .AsNoTracking()
                        .Where(r => r.VehicleId == vehicleId)
                        .ToListAsync();

        public async Task<ServiceRecord> GetByIdAsync(Guid id)
            => await _db.ServiceRecords.FindAsync(id);

        public async Task UpdateAsync(ServiceRecord record)
        {
            _db.ServiceRecords.Update(record);
            await _db.SaveChangesAsync();
        }
    }
}
