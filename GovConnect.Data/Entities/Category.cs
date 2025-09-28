using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class Category : IId, ISoftDelete {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
