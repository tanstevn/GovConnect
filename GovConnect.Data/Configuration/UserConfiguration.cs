using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class UserConfiguration : BaseConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> builder) {
            builder
                .Property(entity => entity.Auth0UserId)
                .IsRequired();

            builder
                .HasIndex(entity => entity.Auth0UserId)
                .IsUnique();

            builder
                .HasOne(entity => entity.Role)
                .WithOne(role => role.User)
                .HasForeignKey<User>(user => user.RoleId)
                .IsRequired();

            builder
                .HasOne(entity => entity.UserDetails)
                .WithOne(userDetails => userDetails.User)
                .HasForeignKey<User>(user => user.UserDetailId)
                .IsRequired();

            builder
                .HasOne(entity => entity.RequestStatusHistory)
                .WithOne(history => history.User);

            builder
                .HasOne(entity => entity.Comment)
                .WithOne(comment => comment.User);

            base.Configure(builder);
        }
    }
}
