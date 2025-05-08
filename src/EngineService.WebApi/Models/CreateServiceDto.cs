// src/EngineService.WebApi/Models/CreateServiceRecordDto.cs
public class CreateServiceRecordDto
{
    public Guid VehicleId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int Mileage { get; set; }
    public string Description { get; set; }
}

// src/EngineService.WebApi/Models/UpdateServiceRecordDto.cs
public class UpdateServiceRecordDto
{
    public DateTime MaintenanceDate { get; set; }
    public int Mileage { get; set; }
    public string Description { get; set; }
}
