using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class User : IId, ISoftDelete {
        public long Id { get; set; }
        public string Auth0dUserId { get; set; }
        public long RoleId { get; set; }
        public long UserDetailId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Role? Role { get; set; }
        public virtual UserDetails? UserDetails { get; set; }
        public virtual ICollection<Request>? Requests { get; set; }
        public virtual RequestStatusHistory? RequestStatusHistory { get; set; }
        public virtual Comment? Comment { get; set; }
    }
}
