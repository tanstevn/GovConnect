using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class City : IId, ISoftDelete {
        public long Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
