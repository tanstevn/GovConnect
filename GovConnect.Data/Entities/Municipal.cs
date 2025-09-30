using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Municipal : IId, ISoftDelete {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
