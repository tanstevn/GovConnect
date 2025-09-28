using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class UserDetailsConfiguration : BaseConfiguration<UserDetails> {
        public override void Configure(EntityTypeBuilder<UserDetails> builder) {
            builder
                .Property(entity => entity.FirstName)
                .IsRequired();

            builder
                .Property(entity => entity.LastName)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
