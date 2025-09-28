using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities
{
    public class Status : IId, ISoftDelete
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short DisplayOrder { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual RequestStatusHistory? RequestStatusHistory { get; set; }
    }
}
