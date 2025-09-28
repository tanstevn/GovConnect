using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class UserConfiguration : BaseConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> builder) {
            builder
                .Property(user => user.Auth0UserId)
                .IsRequired();

            builder
                .HasOne(user => user.Role)
                .WithOne(role => role.User);

            builder
                .HasOne(user => user.UserDetails)
                .WithOne(userDetails => userDetails.User);

            base.Configure(builder);
        }
    }
}
