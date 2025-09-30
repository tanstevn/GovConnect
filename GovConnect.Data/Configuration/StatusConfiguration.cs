using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class StatusConfiguration : BaseConfiguration<Status> {
        public override void Configure(EntityTypeBuilder<Status> builder) {
            builder
                .Property(entity => entity.Name)
                .IsConcurrencyToken();

            builder
                .HasOne(entity => entity.RequestStatusHistory)
                .WithOne(history => history.Status);

            base.Configure(builder);
        }
    }
}
