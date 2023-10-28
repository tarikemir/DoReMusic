using DoReMusic.Domain.Entities;
using DoReMusic.Domain.Enum;

namespace DoReMusic.MVC.ViewModels
{
    public class InstrumentViewModel
    {
        public List<Brand> Brands { get; set; } //Entity
        public List<Category> Categories { get; set; }
    }
}
