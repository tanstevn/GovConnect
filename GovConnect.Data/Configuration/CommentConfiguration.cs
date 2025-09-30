using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class CommentConfiguration : BaseConfiguration<Comment> {
        public override void Configure(EntityTypeBuilder<Comment> builder) {
            builder
                .HasOne(entity => entity.Request)
                .WithMany(request => request.Comments)
                .HasForeignKey(comment => comment.RequestId)
                .IsRequired();

            builder
                .Property(entity => entity.Text)
                .IsRequired();

            builder
                .HasOne(entity => entity.User)
                .WithOne(user => user.Comment)
                .HasForeignKey<Comment>(comment => comment.CommentedBy)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
