using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class PriortyLevelConfiguration : BaseConfiguration<PriorityLevel> {
        public override void Configure(EntityTypeBuilder<PriorityLevel> builder) {
            builder
                .HasOne(entity => entity.Request)
                .WithOne(request => request.PriorityLevel);

            base.Configure(builder);
        }
    }
}
