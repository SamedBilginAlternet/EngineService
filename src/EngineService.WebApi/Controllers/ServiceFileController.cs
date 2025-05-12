// src/EngineService.WebApi/Controllers/ServiceFileController.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EngineService.Domain.Entities;
using EngineService.Domain.Interfaces;
using EngineService.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/vehicles/{vehicleId:guid}/servicerecords/{recordId:guid}/files")]
public class ServiceFileController : ControllerBase
{
    private readonly IServiceFileRepository _repo;
    public ServiceFileController(IServiceFileRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceFileDto>>> GetAll(
        Guid vehicleId, Guid recordId)
    {
        var files = await _repo.GetAllByRecordIdAsync(recordId);
        var dtos = files.Select(f => new ServiceFileDto
        {
            Id = f.Id,
            FileName = f.FileName,
            ContentType = f.ContentType,
            Data = f.Data
        });
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Download(
        Guid vehicleId, Guid recordId, Guid id)
    {
        var file = await _repo.GetByIdAsync(id);
        if (file == null || file.ServiceRecordId != recordId)
            return NotFound();

        return File(file.Data, file.ContentType, file.FileName);
    }

    [HttpPost]
    public async Task<ActionResult> Upload(
        Guid vehicleId, Guid recordId, [FromForm] CreateServiceFileDto dto)
    {
        // IFormFile → byte[]
        using var ms = new MemoryStream();
        await dto.File.CopyToAsync(ms);

        var file = new ServiceFile
        {
            ServiceRecordId = recordId,
            FileName = dto.File.FileName,
            ContentType = dto.File.ContentType,
            Data = ms.ToArray()
        };

        await _repo.AddAsync(file);
        var resultDto = new ServiceFileDto
        {
            Id = file.Id,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Data = file.Data
        };
        return CreatedAtAction(nameof(Download),
            new { vehicleId, recordId, id = file.Id }, resultDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        Guid vehicleId, Guid recordId, Guid id)
    {
        var file = await _repo.GetByIdAsync(id);
        if (file == null || file.ServiceRecordId != recordId)
            return NotFound();

        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
