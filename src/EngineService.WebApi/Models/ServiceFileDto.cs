

// src/EngineService.WebApi/Models/CreateServiceFileDto.cs
public class ServiceFileDto
{
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public string ?ContentType { get; set; }
    public required byte[] Data { get; set; }
}