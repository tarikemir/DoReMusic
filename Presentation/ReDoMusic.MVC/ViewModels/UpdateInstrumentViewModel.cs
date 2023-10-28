using DoReMusic.Domain.Entities;

namespace DoReMusic.MVC.ViewModels
{
    public class UpdateInstrumentViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public string ProductionYear { get; set; }
    }
}
