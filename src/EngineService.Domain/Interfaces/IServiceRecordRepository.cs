// src/EngineService.Domain/Interfaces/IServiceRecordRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EngineService.Domain.Entities;
using EngineService.EngineService.Domain.Entitities;

namespace EngineService.Domain.Interfaces
{
    public interface IServiceRecordRepository
    {
        Task<IEnumerable<ServiceRecord>> GetAllByVehicleAsync(Guid vehicleId);
        Task<ServiceRecord> GetByIdAsync(Guid id);
        Task AddAsync(ServiceRecord record);
        Task UpdateAsync(ServiceRecord record);
        Task DeleteAsync(Guid id);
    }
}
