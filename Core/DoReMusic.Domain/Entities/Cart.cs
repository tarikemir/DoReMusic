using DoReMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoReMusic.Domain.Entities
{
    public class Cart : EntityBase<Guid>
    {
        public Instrument Instrument { get; set; }
        public int Quantity { get; set; }
        //public decimal TotalCartPrice => Quantity * Instrument.Price;
    }
}
