using DoReMusic.Domain.Entities;
using DoReMusic.Domain.Enum;

namespace DoReMusic.MVC.Models
{
    public class UpdateInstrumentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public decimal Price { get; set; }
        public string ProductionYear { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
