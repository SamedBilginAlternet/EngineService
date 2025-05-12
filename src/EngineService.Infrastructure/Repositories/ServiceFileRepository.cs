// src/EngineService.Infrastructure/Repositories/ServiceFileRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineService.Domain.Entities;
using EngineService.Domain.Interfaces;
using EngineService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ServiceFileRepository : IServiceFileRepository
{
    private readonly EngineServiceDbContext _db;
    public ServiceFileRepository(EngineServiceDbContext db) => _db = db;

    public async Task<IEnumerable<ServiceFile>> GetAllByRecordIdAsync(Guid recordId) =>
        await _db.ServiceFiles
                 .AsNoTracking()
                 .Where(f => f.ServiceRecordId == recordId)
                 .ToListAsync();

    public async Task<ServiceFile> GetByIdAsync(Guid id) =>
        await _db.ServiceFiles.FindAsync(id);

    public async Task AddAsync(ServiceFile file)
    {
        file.Id = Guid.NewGuid();
        await _db.ServiceFiles.AddAsync(file);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _db.ServiceFiles.FindAsync(id);
        if (entity == null) return;
        _db.ServiceFiles.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
