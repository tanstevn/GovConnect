using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Role : IId, ISoftDelete {
        public long Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
