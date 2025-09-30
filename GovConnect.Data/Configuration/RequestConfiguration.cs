using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class RequestConfiguration : BaseConfiguration<Request> {
        public override void Configure(EntityTypeBuilder<Request> builder) {
            builder
                .Property(entity => entity.Title)
                .IsRequired();

            builder
                .Property(entity => entity.Description)
                .IsRequired();

            builder
                .HasOne(entity => entity.Subcategory)
                .WithMany(subCategory => subCategory.Requests)
                .HasForeignKey(request => request.SubcategoryId)
                .IsRequired();

            builder
                .HasOne(entity => entity.Barangay)
                .WithMany(barangay => barangay.Requests)
                .HasForeignKey(request => request.BarangayId)
                .IsRequired();

            builder
                .HasMany(entity => entity.RequestAttachments)
                .WithOne(requestAttachment => requestAttachment.Request);

            builder
                .HasMany(entity => entity.RequestStatusHistories)
                .WithOne(history => history.Request);

            builder
                .HasMany(entity => entity.Comments)
                .WithOne(comment => comment.Request);

            base.Configure(builder);
        }
    }
}
