using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class RequestStatusHistory : IId, ISoftDelete {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public short StatusId { get; set; }
        public long UpdatedBy { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Request? Request { get; set; }
        public virtual Status? Status { get; set; }
        public virtual User? User { get; set; }
    }
}
