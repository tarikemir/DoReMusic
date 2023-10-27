using DoReMusic.Domain.Common;
using DoReMusic.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoReMusic.Domain.Entities
{
    public class Instrument : EntityBase<Guid>
    {
        public string Name { get; set; }
        public Brand Brand { get; set; } //Entity
        public Category Category { get; set; }
        public String Kind { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public DateTime? ProductionYear { get; set; }
        public decimal Price { get; set; }

    }
}
