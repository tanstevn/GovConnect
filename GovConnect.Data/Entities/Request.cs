using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Request : IId, ISoftDelete {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long SubcategoryId { get; set; }
        public long BarangayId { get; set; }
        public long RequestedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Subcategory? Subcategory { get; set; }
        public virtual Barangay? Barangay { get; set; }
    }
}
