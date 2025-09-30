using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class CommentAttachment : IId, ISoftDelete {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Comment? Comment { get; set; }
    }
}
