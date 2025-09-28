namespace GovConnect.Data.Abstractions {
    public interface ISoftDelete {
        public DateTime? DateDeleted { get; set; }
    }
}
