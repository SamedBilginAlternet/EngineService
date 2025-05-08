using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EngineService.EngineService.Domain.Entitities;

namespace EngineService.EngineService.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(Guid id);       // Tek araç getirir
        Task<IEnumerable<Vehicle>> GetAllAsync();   // Tüm araçları listeler
        Task AddAsync(Vehicle vehicle);             // Yeni araç ekler
        Task UpdateAsync(Vehicle vehicle);          // Mevcut aracı günceller
        Task DeleteAsync(Guid id);                  // Aracı siler
    }
}
