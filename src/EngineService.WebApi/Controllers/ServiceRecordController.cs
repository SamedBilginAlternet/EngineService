// src/EngineService.WebApi/Controllers/ServiceRecordController.cs
using EngineService.Domain.Interfaces;
using EngineService.EngineService.Domain.Entitities;
using Microsoft.AspNetCore.Mvc;

namespace EngineService.WebApi.Controllers
{
    [ApiController]
    [Route("api/vehicles/{vehicleId:guid}/servicerecords")]
    public class ServiceRecordController : ControllerBase
    {
        private readonly IServiceRecordRepository _repo;
        public ServiceRecordController(IServiceRecordRepository repo) => _repo = repo;

        // GET: api/vehicles/{vehicleId}/servicerecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecord>>> GetAll(Guid vehicleId)
            => Ok(await _repo.GetAllByVehicleAsync(vehicleId));

        // GET: api/vehicles/{vehicleId}/servicerecords/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ServiceRecord>> Get(Guid vehicleId, Guid id)
        {
            var rec = await _repo.GetByIdAsync(id);
            if (rec == null || rec.VehicleId != vehicleId) return NotFound();
            return Ok(rec);
        }

        // POST: api/vehicles/{vehicleId}/servicerecords
        [HttpPost]
        public async Task<ActionResult> Create(Guid vehicleId, [FromBody] CreateServiceRecordDto dto)
        {
            var rec = new ServiceRecord
            {
                VehicleId = vehicleId,
                MaintenanceDate = dto.MaintenanceDate,
                Mileage = dto.Mileage,
                Description = dto.Description
            };
            await _repo.AddAsync(rec);
            return CreatedAtAction(nameof(Get),
                new { vehicleId, id = rec.Id }, rec);
        }

        // PUT: api/vehicles/{vehicleId}/servicerecords/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid vehicleId,
            Guid id,
            [FromBody] UpdateServiceRecordDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null || existing.VehicleId != vehicleId)
                return NotFound();

            existing.MaintenanceDate = dto.MaintenanceDate;
            existing.Mileage = dto.Mileage;
            existing.Description = dto.Description;

            await _repo.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/vehicles/{vehicleId}/servicerecords/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid vehicleId, Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null || existing.VehicleId != vehicleId)
                return NotFound();

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
