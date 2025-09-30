using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class BarangayConfiguration : BaseConfiguration<Barangay> {
        public override void Configure(EntityTypeBuilder<Barangay> builder) {
            builder
                .Property(entity => entity.Name)
                .IsRequired();

            builder
                .HasMany(entity => entity.Requests)
                .WithOne(request => request.Barangay);

            base.Configure(builder);
        }
    }
}
