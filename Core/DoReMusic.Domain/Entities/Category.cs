using DoReMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoReMusic.Domain.Entities
{
    public class Category : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public List<String>? Kinds { get; set; }
        public Category()
        {
            Id = Guid.NewGuid(); // Generate a unique identifier
            CreatedOn = DateTime.UtcNow; // Set the creation date to the current time in UTC
        }
    }
}
