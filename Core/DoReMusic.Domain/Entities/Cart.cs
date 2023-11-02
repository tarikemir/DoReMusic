using DoReMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoReMusic.Domain.Entities
{
    public class Cart: EntityBase<Guid>
    {
        public List<CartItem> Items { get; set; }
    }
}
