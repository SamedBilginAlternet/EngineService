// src/EngineService.WebApi/Models/VehicleDto.cs
public class CreateVehicleDto
{
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
}
