// src/EngineService.WebApi/Controllers/VehicleController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EngineService.Domain.Entities;

using EngineService.EngineService.Domain.Entitities;
using EngineService.EngineService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EngineService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repo;
        public VehicleController(IVehicleRepository repo)
            => _repo = repo;

        // GET: api/vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        // GET: api/vehicle/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Vehicle>> GetById(Guid id)
        {
            var v = await _repo.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        // POST: api/vehicle
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateVehicleDto dto)
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year
            };
            await _repo.AddAsync(vehicle);
            return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
        }

        // PUT: api/vehicle/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id) return BadRequest("ID mismatch");
            var exists = await _repo.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _repo.UpdateAsync(vehicle);
            return NoContent();
        }

        // DELETE: api/vehicle/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exists = await _repo.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
