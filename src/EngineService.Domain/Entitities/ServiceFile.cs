using EngineService.EngineService.Domain.Entitities;

namespace EngineService.Domain.Entities
{
    public class ServiceFile
    {
        public Guid Id { get; set; }
        public Guid ServiceRecordId { get; set; }
        public ServiceRecord ServiceRecord { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
    }
}
