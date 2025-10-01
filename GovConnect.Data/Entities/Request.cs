using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Request : IId, ISoftDelete {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long SubcategoryId { get; set; }
        public long BarangayId { get; set; }
        public long RequestedByUserId { get; set; }
        public long PriorityLevelId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Subcategory? Subcategory { get; set; }
        public virtual Barangay? Barangay { get; set; }
        public virtual User? User { get; set; }
        public virtual PriorityLevel? PriorityLevel { get; set; }
        public virtual ICollection<RequestAttachment>? RequestAttachments { get; set; }
        public virtual ICollection<RequestStatusHistory>? RequestStatusHistories { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
