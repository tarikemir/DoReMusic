using DoReMusic.Domain.Entities;

namespace DoReMusic.MVC.ViewModels
{
    public class SearchInstrumentViewmodel
    {
        public string search { get; set; }
        public List<Instrument> Instruments { get; set; }
    }
}
