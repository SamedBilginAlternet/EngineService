// src/EngineService.Domain/Interfaces/IServiceFileRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EngineService.Domain.Entities;

public interface IServiceFileRepository
{
    Task<IEnumerable<ServiceFile>> GetAllByRecordIdAsync(Guid recordId);
    Task<ServiceFile> GetByIdAsync(Guid id);
    Task AddAsync(ServiceFile file);
    Task DeleteAsync(Guid id);
}
