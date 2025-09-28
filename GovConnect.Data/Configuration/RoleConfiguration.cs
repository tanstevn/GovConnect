using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class RoleConfiguration : BaseConfiguration<Role> {
        public override void Configure(EntityTypeBuilder<Role> builder) {
            builder
                .Property(entity => entity.Code)
                .IsRequired();

            builder
                .Property(entity => entity.Name)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
