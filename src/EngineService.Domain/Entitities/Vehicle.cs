namespace EngineService.EngineService.Domain.Entitities
{
    public class Vehicle
    {
        public Guid Id { get; set; }                 // Benzersiz kimlik
        public string Brand { get; set; }            // Marka adı
        public string Model { get; set; }            // Model adı
        public int Year { get; set; }                // Üretim yılı
        public ICollection<ServiceRecord> ServiceRecords { get; set; }
            = new List<ServiceRecord>();           // Bu araca ait bakım kayıtları
    }
}

