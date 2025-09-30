using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class CommentAttachmentConfiguration : BaseConfiguration<CommentAttachment> {
        public override void Configure(EntityTypeBuilder<CommentAttachment> builder) {
            builder
                .HasOne(entity => entity.Comment)
                .WithMany(comment => comment.CommentAttachments)
                .HasForeignKey(attachment => attachment.CommentId)
                .IsRequired();

            builder
                .Property(entity => entity.FileName)
                .IsRequired();

            builder
                .Property(entity => entity.FileUrl)
                .IsRequired();

            builder
                .Property(entity => entity.ContentType)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
