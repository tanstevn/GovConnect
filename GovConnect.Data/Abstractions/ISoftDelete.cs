namespace GovConnect.Data.Abstractions {
    public interface ISoftDelete {
        public DateTime? DeletedAt { get; set; }
    }
}
