using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class PriorityLevel : IId {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual Request? Request { get; set; }
    }
}
