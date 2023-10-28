using DoReMusic.Domain.Entities;
using DoReMusic.Domain.Enum;

namespace DoReMusic.MVC.Models
{
    public class AddInstrumentRequest
    {
        public string Name { get; set; }
        public string Brand { get; set; } //Entity
        public string Category { get; set; }
        public String Kind { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductionYear { get; set; }
    }
}
