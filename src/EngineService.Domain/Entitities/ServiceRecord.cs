using EngineService.Domain.Entities;

namespace EngineService.EngineService.Domain.Entitities
{
    
     public class ServiceRecord
    {
        public Guid Id { get; set; }                 // Kayıt kimliği
        public Guid VehicleId { get; set; }          // İlgili aracın Id’si
        public Vehicle ?Vehicle { get; set; }         // Navigasyon property
        public DateTime MaintenanceDate { get; set; }// Bakım tarihi
        public int Mileage { get; set; }             // Bakımdaki kilometre
        public string ?Description { get; set; }      // Açıklama
        public ICollection<ServiceFile> Files { get; set; }
            = new List<ServiceFile>();             // İlişkili dosyalar (resim/pdf)
    }
}
