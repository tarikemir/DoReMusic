using DoReMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusicMarket.Domain.Common
{
    public class Category : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
