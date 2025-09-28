using GovConnect.Data.Abstractions;

namespace GovConnect.Data.Entities {
    public class UserDetails : IId, ISoftDelete {
        public long Id { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
