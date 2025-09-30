using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class RequestAttachment : IId, ISoftDelete {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Request? Request { get; set; }
    }
}
