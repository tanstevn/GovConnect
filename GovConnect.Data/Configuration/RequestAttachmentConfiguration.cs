using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class RequestAttachmentConfiguration : BaseConfiguration<RequestAttachment> {
        public override void Configure(EntityTypeBuilder<RequestAttachment> builder) {
            builder
                .HasOne(entity => entity.Request)
                .WithMany(request => request.RequestAttachments)
                .HasForeignKey(requestAttachment => requestAttachment.RequestId)
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
