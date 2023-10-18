using DoReMusic.Domain.Common;

namespace DoReMusic.Domain.Entities
{
    public class Brand : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public string Address { get; set; }
    }
}