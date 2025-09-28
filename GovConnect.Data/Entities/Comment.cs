using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Comment : IId, ISoftDelete {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Text { get; set; }
        public long CommentedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Request? Request { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CommentAttachment> CommentAttachments { get; set; }
    }
}
