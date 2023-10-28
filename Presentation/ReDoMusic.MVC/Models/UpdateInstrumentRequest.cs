using DoReMusic.Domain.Enum;

namespace DoReMusic.MVC.Models
{
    public class UpdateInstrumentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public decimal Price { get; set; }
        public string ProductionYear { get; set; }
    }
}
